﻿using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.BookingService;
using PhotoboothBranchService.Application.Services.AccountServices;
using PhotoboothBranchService.Application.Services.BookingServiceServices;
using PhotoboothBranchService.Application.Services.EmailServices;
using PhotoboothBranchService.Application.Services.RefundServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Application.Services.BookingServices;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;
    private readonly IBoothRepository _boothRepository;
    private readonly IBookingServiceService _bookingServiceService;
    private readonly IAccountService _accountService;
    private readonly IRefundService _refundService;
    private readonly IFullPaymentPolicyRepository _fullPaymentPolicyRepository;
    private readonly IEmailService _emailService;
    private readonly ISlotRepository _slotRepository;
    private readonly IBookingSlotRepository _bookingSlotRepository;
    public BookingService(IBookingRepository sessionOrderRepository,
        IMapper mapper,
        IBoothRepository boothRepository,
        IRefundService refundService,
        IFullPaymentPolicyRepository fullPaymentPolicyRepository,
        ISlotRepository slotRepository,
        IEmailService emailService,
        IBookingSlotRepository bookingSlotRepository,
        IBookingServiceService bookingServiceService,
        IAccountService accountService)
    {
        _bookingRepository = sessionOrderRepository;
        _mapper = mapper;
        _boothRepository = boothRepository;
        _accountService = accountService;
        _refundService = refundService;
        _fullPaymentPolicyRepository = fullPaymentPolicyRepository;
        _emailService = emailService;
        _slotRepository = slotRepository;
        _bookingSlotRepository = bookingSlotRepository;
        _bookingServiceService = bookingServiceService;
    }

    // Create a new session
    public async Task<CreateBookingResponse> CreateAsync(BookingRequest createModel, BookingType bookingType)
    {
        // Validate customer
        var account = await _accountService.ValidateCustomerAsync(createModel.CustomerPhoneNumber, createModel.CustomerEmail);

        // Validate booth
        var booth = await ValidateBoothAsync(createModel.BoothID);
        // Map to booking entity
        var booking = _mapper.Map<Booking>(createModel);
        // Validate time range
        ValidateTimeRange(booking.StartTime, booking.EndTime, booth.Branch.OpeningTime, booth.Branch.ClosingTime);

        // Validate booking time conflicts
        var checkBooking = await ValidateBookingTime(createModel.BoothID, booking.StartTime, booking.EndTime);
        if (checkBooking.Count() != 0)
        {
            throw new BadRequestException("There is another booking in the selected time range, please choose another time");
        }
        // Calculate payment amount
        //var processedServices = await ProcessServiceListAsync(createModel.ServiceList);
        var processedServices = await _bookingServiceService.CreateServiceListForNewBooking(createModel.ServiceList);
        var processSlot = await this.ProcessHireBoothFee(booth.BoothID, createModel.Date, createModel.StartTime, createModel.EndTime);
       //add the remain field
        booking.CustomerID = account.AccountID;
        booking.ValidateCode = await GenerateValidateCode();
        booking.BookingStatus = BookingStatus.PendingPayment;
        booking.CreatedDate = DateTimeHelper.GetVietnamTimeNow();
        booking.HireBoothFee = processSlot.Item1;
        booking.TotalPrice = processedServices.Item1 + booking.HireBoothFee;
        booking.BookingType = bookingType;
        booking.BookingServices = processedServices.Item2;
        booking.PaymentStatus = PaymentStatus.Processing;
        booking.CustomerBusinessID = this.GenerateCustomerReferenceID(account.FirstName, account.LastName);
        booking.FullPaymentPolicyID = await GetApplicablePolicyIdAsync(booking.StartTime);
        booking.BookingSlots = processSlot.Item2;
        // Save booking
        await _bookingRepository.AddAsync(booking);
        var list = await _bookingServiceService.GetByBookingIdAsync(booking.BookingID);
        booking.BookingServices = list.ToList();
        return _mapper.Map<CreateBookingResponse>(booking);
    }
    public async Task<BookingResponse> AddExtraService(AddExtraServiceRequest request)
    {
        //find now session order of request booth
        var booking = (await _bookingRepository
            .GetAsync(i => i.BoothID == request.BoothID
                && (i.BookingStatus == BookingStatus.TakingPhoto || i.BookingStatus == BookingStatus.ExtraService)
                && i.EndTime > DateTimeHelper.GetVietnamTimeNow()
                && DateTimeHelper.GetVietnamTimeNow() > i.StartTime))
            .FirstOrDefault();
        if (booking == null)
        {
            throw new NotFoundException("Not found booking running in this booth");
        }
        if (request.BookingID != booking.BookingID)
        {
            throw new Exception("Error on database");
        }

        var result = await _bookingServiceService.AddExtraService(new AddListBookingServiceRequest
        {
            BookingID = request.BookingID,
            ServiceList = request.ServiceList
        });
        booking.TotalPrice += result;
        booking.BookingStatus = BookingStatus.ExtraService;
        booking.PaymentStatus = PaymentStatus.PendingPayExtra;
        await _bookingRepository.UpdateAsync(booking);
        return await this.GetByIdAsync(booking.BookingID);
    }
    private async Task<Guid> GetApplicablePolicyIdAsync(DateTime startTime)
    {
        var policies = await _fullPaymentPolicyRepository.GetAsync(p => p.IsActive && (p.StartDate == null || p.StartDate <= DateOnly.FromDateTime(startTime)) && (p.EndDate == null || p.EndDate >= DateOnly.FromDateTime(startTime)));
        var policy = policies.FirstOrDefault();
        if (policy == null)
        {
            throw new Exception("Not found policy");
        }
        return policy.FullPaymentPolicyID;
    }
    public async Task<CreateBookingResponse> CustomerBooking(CustomerBookingRequest request, string email)
    {
        //if ((request.StartTime - DateTimeHelper.GetVietnamTimeNow()).TotalMinutes <= 30)
        //{
        //    throw new BadRequestException("You must booking a session with start time at least 30 minutes from now");
        //}
        var createSessionOrderRequest = _mapper.Map<BookingRequest>(request);
        createSessionOrderRequest.CustomerEmail = email;
        return await CreateAsync(createSessionOrderRequest, BookingType.Online);
    }
    public async Task<CreateBookingResponse> GuestBooking(GuestBookingRequest request)
    {
        AccountRegisterResponse account;
        try
        {
            var accountCheck = await _accountService.ValidateCustomerAsync(request.CustomerPhoneNumber, request.CustomerEmail);
            throw new BadRequestException("Account existed, can not use this function");
        } catch (NotFoundException)
        {
            //create new account for guest
            CreateAccountRequestModel requestNewAccount = new CreateAccountRequestModel
            {
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                Email = request.CustomerEmail,
                FirstName = request.FirstName,
                LastName = request.LasttName,
                PhoneNumber = request.CustomerPhoneNumber,
                Password = PasswordGenerator.GeneratePassword(23),
            };
            requestNewAccount.ConfirmPassword = requestNewAccount.Password;
            account = await _accountService.Register(requestNewAccount, AccountRole.Customer);
        }
        //create booking request
        BookingRequest bookingRequest = new BookingRequest
        {
            BoothID = request.BoothID,
            CustomerEmail = request.CustomerEmail,
            Date = request.Date,
            ServiceList = request.ServiceList,
            EndTime = request.EndTime,
            StartTime = request.StartTime,
        };

        try //start booking
        {
            var response =  await this.CreateAsync(bookingRequest, BookingType.Staff);
            string link = await _accountService.ResetPassword(request.CustomerEmail);
            await _emailService.SendAutoRegistEmailNoti(request.CustomerEmail, link, $"{account.FirstName} {account.LastName}");
            return response;
        }
        catch (Exception ex)
        {
            await _accountService.DeleteAsync(account.AccountID);
            if (ex.InnerException != null)
            {
                throw ex.InnerException;
            }
            else
            {
                throw;
            }
        }
    }
    public async Task<BookingResponse> Checkin(CheckinCodeRequest validateSessionPhotoRequest)
    {
        var booking = await _bookingRepository.GetBookingByValidateCodeAndBoothIdAsync(
            validateSessionPhotoRequest.Code, validateSessionPhotoRequest.BoothID
        );

        if (booking == null)
        {
            throw new BadRequestException("Wrong validate code, please try again");
        }

        if (booking.BookingStatus == BookingStatus.Canceled)
        {
            throw new BadRequestException("This booking has been cancelled. Please contact our staff for further assistance.");
        }
        var timeNow = DateTimeHelper.GetVietnamTimeNow();
        if (booking.EndTime <= timeNow)
        {
            throw new BadRequestException("This booking has already ended. Please contact our staff for further assistance.");
        }

        if (booking.PaymentStatus != PaymentStatus.Paid)
        {
            throw new BadRequestException("This booking has not been paid for. Please complete the payment to proceed.");
        }

        if (booking.StartTime > timeNow)
        {
            throw new BadRequestException("The time for your session has not come yet, please check with our staff and try again later");
        }

        // Ensure services are loaded correctly
        //if (booking.BookingServices == null || !booking.BookingServices.Any() || booking.BookingServices.Any(bs => bs.Service == null))
        //{
        //    throw new InvalidOperationException("Booking services not loaded correctly");
        //}

        booking.BookingStatus = BookingStatus.TakingPhoto;
        await _bookingRepository.UpdateAsync(booking);
        return await this.GetByIdAsync(booking.BookingID);
    }
    // Delete a session by ID
    public async Task DeleteAsync(Guid id)
    {
        var session = (await _bookingRepository.GetAsync(s => s.BookingID == id)).FirstOrDefault();
        if (session != null)
        {
            await _bookingRepository.RemoveAsync(session);
        }
        else
        {
            throw new KeyNotFoundException("Session not found.");
        }
    }

    // Get all sessions
    public async Task<IEnumerable<BookingResponse>> GetAllAsync()
    {
        var bookings = (await _bookingRepository.GetAsync(null, includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
                i => i.FullPaymentPolicy
            })).ToList();
        foreach (var booking in bookings)
        {
            var list = await _bookingServiceService.GetByBookingIdAsync(booking.BookingID);
            booking.BookingServices = list.ToList();
        }
        return _mapper.Map<IEnumerable<BookingResponse>>(bookings.ToList().OrderByDescending(i => i.StartTime));
    }

    public async Task<IEnumerable<BookingResponse>> GetAllPagingAsync(BookingFilter filter, PagingModel paging)
    {
        var sessions = (await _bookingRepository.GetAsync(null, includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
                i => i.FullPaymentPolicy
            })).ToList().AutoFilter(filter);
        var listSessionresponse = _mapper.Map<IEnumerable<BookingResponse>>(sessions);
        return listSessionresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).ToList().OrderByDescending(i => i.StartTime);
    }
    public async Task<IEnumerable<BookingResponse>> GetBoothFutureBooking(Guid boothID)
    {
        var bookings = (await _bookingRepository.GetAsync(i => i.BoothID == boothID && i.StartTime > DateTimeHelper.GetVietnamTimeNow())).ToList();
        return bookings.Any() ? _mapper.Map<IEnumerable<BookingResponse>>(bookings) : Enumerable.Empty<BookingResponse>();
    }
    public async Task<IEnumerable<BookingResponse>> GetBranchFutureBooking(Guid branchID)
    {
        var boothIds = (await _boothRepository.GetAsync(i => i.BranchID == branchID)).Select(i => i.BoothID).ToList();
        var bookings = (await _bookingRepository.GetAsync(i => boothIds.Contains(i.BoothID) && i.StartTime > DateTimeHelper.GetVietnamTimeNow())).ToList();
        return bookings.Any() ? _mapper.Map<IEnumerable<BookingResponse>>(bookings) : Enumerable.Empty<BookingResponse>();
    }
    // Get a session by ID
    public async Task<BookingResponse> GetByIdAsync(Guid id)
    {
        var booking = (await _bookingRepository.GetAsync(s => s.BookingID == id,
            includeProperties: new Expression<Func<Booking, object>>[]
            {
                 i => i.FullPaymentPolicy,
            })).FirstOrDefault();

        if (booking == null)
        {
            throw new NotFoundException("Booking not found.");
        }
        var slotList = await _bookingSlotRepository.GetAsync(i => i.BookingID == booking.BookingID, i => i.Slot);
        booking.BookingSlots = slotList.ToList();
        var list = await _bookingServiceService.GetByBookingIdAsync(booking.BookingID);
        booking.BookingServices = list.ToList();
        return _mapper.Map<BookingResponse>(booking);
    }
    public async Task<IEnumerable<BookingResponse>> SearchByReferenceIDAsync(string id)
    {
        var bookings = (await _bookingRepository.GetAsync(s => s.CustomerBusinessID.Contains(id),
            includeProperties: new Expression<Func<Booking, object>>[]
            {
                 i => i.FullPaymentPolicy
            })).ToList();

        if (!bookings.Any())
        {
            throw new KeyNotFoundException("Booking not found.");
        }
        foreach (var booking in bookings)
        {
            var list = await _bookingServiceService.GetByBookingIdAsync(booking.BookingID);
            booking.BookingServices = list.ToList();
        }
        
        return _mapper.Map<IEnumerable<BookingResponse>>(bookings);
    }
    // Update a session
    public async Task<CreateBookingResponse> UpdateAsync(Guid id, UpdateBookingRequest updateModel, string? email)
    {
        var updateBooking = _mapper.Map<Booking>(updateModel);
        var booking = (await _bookingRepository.GetAsync(i => i.BookingID == id)).FirstOrDefault();
        if (booking == null)
        {
            throw new NotFoundException("Not found Booking");
        }
        var account = await _accountService.ValidateCustomerAsync("", email);
        if (account.Role == AccountRole.Customer)
        {
            if (booking.CustomerID != account.AccountID)
            {
                throw new ForbiddenAccessException("Can not update booking on another cusotmer.");
            }
        }
        if (booking.BookingStatus != BookingStatus.PendingPayment && booking.PaymentStatus != PaymentStatus.Processing)
        {
            throw new BadRequestException("Cannot update the booking, it's already pay or has been cancelled");
        }
        var booth = await ValidateBoothAsync(updateModel.BoothID);
        ValidateTimeRange(updateBooking.StartTime, updateBooking.EndTime, booth.Branch.OpeningTime, booth.Branch.ClosingTime);
        var checkBooking = await ValidateBookingTime(updateModel.BoothID, updateBooking.StartTime, updateBooking.EndTime);
        if (checkBooking.Count() > 1 || checkBooking[0].BookingID != booking.BookingID)
        {
            throw new BadRequestException("There is another booking in the selected time range, please choose another time");
        }
        var processedServices = /*await ProcessServiceListAsync(updateModel.ServiceList);*/ await _bookingServiceService.CreateServiceListForNewBooking(updateModel.ServiceList);
        var processSlot = await this.ProcessHireBoothFee(booth.BoothID, updateModel.Date, updateModel.StartTime, updateModel.EndTime);
        // Map to booking entity
        booking.StartTime = updateBooking.StartTime;
        booking.EndTime = updateBooking.EndTime;
        booking.ValidateCode = await GenerateValidateCode();
        booking.BookingStatus = BookingStatus.PendingPayment;
        booking.CreatedDate = DateTimeHelper.GetVietnamTimeNow();
        booking.HireBoothFee = processSlot.Item1;
        booking.TotalPrice = processedServices.Item1 + booking.HireBoothFee;
        string firstName, lastName;
        if (account.Role == AccountRole.Customer)
        {
            firstName = account.FirstName;
            lastName = account.LastName;
        } else
        {
            var res = await _accountService.GetByIdAsync(booking.CustomerID);
            firstName = res.FirstName;
            lastName = res.LastName;
        }
        booking.CustomerBusinessID = this.GenerateCustomerReferenceID(firstName, lastName);
        booking.FullPaymentPolicyID = await GetApplicablePolicyIdAsync(updateBooking.StartTime);
        //delete old booking service
        await _bookingServiceService.DeleteByBookingIdAsync(booking.BookingID);
        // and add new
        var bookingServices = processedServices.Item2.ToList();
        await _bookingServiceService.AddByList(bookingServices, booking.BookingID);

        //delete booking slot
        var bookingSlots = (await _bookingSlotRepository.GetAsync(i => i.BookingID == booking.BookingID)).ToList();
        var removalTasks = bookingSlots.Select(bookingSlot =>
            _bookingSlotRepository.RemoveAsync(bookingSlot)
        );
        await Task.WhenAll(removalTasks);
        // and add new
        bookingSlots = processSlot.Item2.ToList();
        var addTasks = bookingSlots.Select(async bookingSlot =>
        {
            bookingSlot.BookingID = booking.BookingID;
            await _bookingSlotRepository.AddAsync(bookingSlot);
        });
        await Task.WhenAll(addTasks);
        // Save booking
        await _bookingRepository.UpdateAsync(booking);

        var list = await _bookingServiceService.GetByBookingIdAsync(booking.BookingID);
        booking.BookingServices = list.ToList();
        return _mapper.Map<CreateBookingResponse>(booking);
    }
    public async Task<CancelBookingResponse> CancelBooking(Guid bookingID, string? ipAddress, string? email)
    {
        CancelBookingResponse response = new CancelBookingResponse();
        var timeNow = DateTimeHelper.GetVietnamTimeNow();
        var booking = (await _bookingRepository.GetAsync(i => i.BookingID == bookingID, i => i.FullPaymentPolicy)).FirstOrDefault();
        if (null == booking)
        {
            response.message = "Booking not found";
        }
        else if (booking.BookingStatus == BookingStatus.TakingPhoto || booking.BookingStatus == BookingStatus.ExtraService)
        {
            response.message = "Booking is going, cannot cancel.";
        }
        else if (booking.BookingStatus == BookingStatus.Canceled)
        {
            response.message = "Booking already canceled.";
        }
        else if (booking.EndTime < timeNow || booking.BookingStatus == BookingStatus.NoShow || booking.BookingStatus == BookingStatus.CompleteChecked)
        {
            response.message = "Booking has ended, can not cancel.";
        }
        else
        {
            try
            {
                if (booking.StartTime > timeNow)
                {
                    if (booking.BookingStatus == BookingStatus.PendingChecking && (booking.StartTime.Date - timeNow).TotalDays > booking.FullPaymentPolicy.RefundDaysBefore)
                    {
                        //doing refund
                        var refundRes = await _refundService.RefundByBookingID(bookingID, false, ipAddress, email);
                        response.isRefund = true;
                        response.refundList = refundRes.ToList();
                        if (response.refundList.Any(i => i.Status == RefundStatus.Fail))
                        {
                            response.message = "Success cancel booking but not refund success, please contact our staff.";
                            booking.PaymentStatus = PaymentStatus.PendingRefund;
                        }
                        else
                        {
                            response.message = $"Cancel booking and the refund successfully, with {booking.FullPaymentPolicy.RefundPercent}% value of booking";
                            booking.PaymentStatus = PaymentStatus.Refunded;
                        }
                    }
                    else
                    {
                        response.message = $"Cancel booking successfully, but the cancel date does not meet our policy (must be more than {booking.FullPaymentPolicy.RefundDaysBefore} days before) to be eligible for a refund.";
                    }
                }
                else
                {
                    response.message = $"Cancel booking successfully, but the cancel date is not meet our policy (must before {booking.FullPaymentPolicy.RefundDaysBefore}) to refund.";
                }
                var booth = (await _boothRepository.GetAsync(i => i.BoothID == booking.BoothID)).FirstOrDefault();
                if (booth != null && booth.Status == BoothStatus.Booked && timeNow > booking.StartTime && timeNow < booking.EndTime) 
                { 
                    booth.Status = BoothStatus.Active;
                    await _boothRepository.UpdateAsync(booth);
                }
                response.isSuccess = true;
                booking.BookingStatus = BookingStatus.Canceled;
                booking.CancelledDate = DateTimeHelper.GetVietnamTimeNow();
                await _bookingRepository.UpdateAsync(booking);
                await _emailService.SendCancelBookingInformation(bookingID);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
        }
        return response;
    }
    //private method
    private void ValidateTimeRange(DateTime startTime, DateTime endTime, TimeSpan openTime, TimeSpan closeTime)
    {
        if (startTime >= endTime)
        {
            throw new BadRequestException("Start time must be before end time.");
        }

        if (startTime < DateTimeHelper.GetVietnamTimeNow())
        {
            throw new BadRequestException("Start time cannot be in the past.");
        }
        DateTime lowerBound = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0).Add(openTime);
        DateTime upperBound = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0).Add(closeTime);

        bool isSameDate = startTime.Date == endTime.Date;
        bool isStartTimeValid = startTime >= lowerBound && startTime <= upperBound;
        bool isEndTimeValid = endTime >= lowerBound && endTime <= upperBound;

        if (!isSameDate)
        {
            throw new BadRequestException("Start time and End time must in the same date.");
        }
        if (!isStartTimeValid || !isEndTimeValid)
        {
            throw new BadRequestException($"Booking time must be within business hours. From {openTime} to {closeTime}");
        }
    }
    private async Task<List<Booking>> ValidateBookingTime(Guid boothId, DateTime startTime, DateTime endTime)
    {
        var bookings = (await _bookingRepository.GetAsync(i => i.BoothID == boothId
                                 && ((startTime < i.StartTime && i.StartTime < endTime) || (endTime > i.EndTime && i.EndTime > startTime))
                                 && i.BookingStatus != BookingStatus.Canceled)).ToList();
        return bookings?.ToList() ?? new List<Booking>();
    }
    private async Task<(decimal, List<BookingSlot>)> ProcessHireBoothFee(Guid BoothID, DateOnly date, TimeSpan startTime,  TimeSpan endTime)
    {
        var slots = (await _slotRepository.GetAsync(i => i.SlotEndTime <= endTime && i.SlotStartTime >= startTime && i.BoothID == BoothID)).OrderBy(i => i.SlotStartTime).ToList();
        decimal fee = 0;
        if (!slots.Any()) 
        {
            throw new NotFoundException("Not found any slot");
        }
        var bookingSlotList = new List<BookingSlot>();
        TimeSpan checkTime = slots[0].SlotStartTime;
        foreach (var slot in slots)
        {
            fee += slot.Price;
            checkTime = checkTime == slot.SlotStartTime ? slot.SlotEndTime : throw new BadRequestException("Slot list is not in sequence by time");
            bookingSlotList.Add(new BookingSlot
            {
                Price = slot.Price,
                SlotID = slot.SlotID,
                BookingDate = date,
            });
        }
        return (fee, bookingSlotList);
    }
    private async Task<long> GenerateValidateCode()
    {
        long code = 0;
        var timeNow = DateTimeHelper.GetVietnamTimeNow();
        var existedCodes = (await _bookingRepository.GetAsync(i => i.BookingStatus != BookingStatus.Canceled && (i.StartTime > timeNow || i.EndTime > timeNow))).ToList().Select(i => i.ValidateCode);
        while (code == 0)
        {
            code = new Random().Next(100000, 1000000);
            if (existedCodes.Any(i => i == code))
            {
                code = 0;
            }
        }
        return code;
    }
    public string GenerateCustomerReferenceID(string firstName, string lastName)
    {
        // Combine firstName and lastName
        string fullName = $"{firstName} {lastName}";

        // Extract the first letter of each word in the full name
        string initials = string.Concat(fullName.Split(' ')
            .Where(word => !string.IsNullOrEmpty(word))
            .Select(word => word[0]))
            .ToUpper();

        string datePart = DateTimeHelper.GetVietnamTimeNow().ToString("yyyyMMdd");
        string randomPart = new Random().Next(10009, 99999).ToString();

        return $"{initials}-{datePart}-{randomPart}";
    }
    private async Task<Booth> ValidateBoothAsync(Guid boothID)
    {
        var booth = (await _boothRepository.GetAsync(i => i.BoothID == boothID, i => i.Branch)).FirstOrDefault();
        if (booth == null)
        {
            throw new NotFoundException("Booth not found on server, try again later");
        }
        else if (booth.Status == BoothStatus.Booked || booth.Status == BoothStatus.Inactive)
        {
            throw new BadRequestException("Booth is used by another or is inactive, in maintenance");
        }
        else if (booth.Branch.Status == BranchStatus.Inactive)
        {
            throw new BadRequestException("Branch of this booth has been closed, please try another branch");
        }

        return booth;
    }
}

