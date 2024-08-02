using AutoMapper;
using Newtonsoft.Json.Linq;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.BookingService;
using PhotoboothBranchService.Application.Services.BookingServiceServices;
using PhotoboothBranchService.Application.Services.RefundServices;
using PhotoboothBranchService.Application.Services.TransactionServices;
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
    private readonly ITransactionService _transactionService;
    private readonly IBookingServiceRepository _bookingServiceRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRefundService _refundService;
    private readonly IServiceRepository _serviceRepository;
    private readonly IFullPaymentPolicyRepository _fullPaymentPolicyRepository;
    public BookingService(IBookingRepository sessionOrderRepository,
        IMapper mapper,
        IBoothRepository boothRepository,
        ITransactionService paymentService,
        IBookingServiceRepository bookingServiceRepository,
        IAccountRepository accountRepository,
        IRefundService refundService, 
        IServiceRepository serviceRepository,
        IFullPaymentPolicyRepository fullPaymentPolicyRepository)
    {
        _bookingRepository = sessionOrderRepository;
        _mapper = mapper;
        _boothRepository = boothRepository;
        _transactionService = paymentService;
        _bookingServiceRepository = bookingServiceRepository;
        _accountRepository = accountRepository;
        _refundService = refundService;
        _bookingServiceRepository = bookingServiceRepository;
        _serviceRepository = serviceRepository;
        _fullPaymentPolicyRepository = fullPaymentPolicyRepository;
    }

    // Create a new session
    public async Task<CreateBookingResponse> CreateAsync(BookingRequest createModel, BookingType bookingType)
    {
        // Validate customer
        var account = await ValidateCustomerAsync(createModel.CustomerPhoneNumber, createModel.CustomerEmail);

        // Validate booth
        var booth = await ValidateBoothAsync(createModel.BoothID);

        // Validate time range
        ValidateTimeRange(createModel.StartTime, createModel.EndTime, booth.Branch.OpeningTime, booth.Branch.ClosingTime);

        // Validate booking time conflicts
        await ValidateBookingTime(createModel.BoothID, createModel.StartTime, createModel.EndTime);

        // Calculate payment amount
        var processedServices = await ProcessServiceListAsync(booth.PricePerHour, createModel.StartTime, createModel.EndTime, createModel.ServiceList);

        // Map to booking entity
        var booking = _mapper.Map<Booking>(createModel);
        booking.CustomerID = account.AccountID;
        booking.ValidateCode = await GenerateValidateCode();
        booking.Status = BookingStatus.PendingPayment;
        booking.IsCancelled = false;
        booking.CreatedDate = DateTimeHelper.GetVietnamTimeNow();
        booking.HireBoothFee = processedServices.Item3;
        booking.PaymentAmount = processedServices.Item1;
        booking.BookingType = bookingType;
        booking.BookingServices = processedServices.Item2;
        booking.PaymentStatus = PaymentStatus.Processing;
        // Set payment policy if applicable
        //if (booking.BookingType == BookingType.Online)
        //{
        booking.FullPaymentPolicyID = await GetApplicablePolicyIdAsync(createModel.StartTime);
        //}
        // Save booking
        await _bookingRepository.AddAsync(booking);
        var list =  await _bookingServiceRepository.GetAsync(i => i.BookingID == booking.BookingID, i => i.Service);
        booking.BookingServices = list.ToList();
        return _mapper.Map<CreateBookingResponse>(booking);
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
        if ((request.StartTime - DateTimeHelper.GetVietnamTimeNow()).TotalMinutes <= 30)
        {
            throw new BadRequestException("You must booking a session with start time at least 30 minutes from now");
        }
        var createSessionOrderRequest = _mapper.Map<BookingRequest>(request);
        createSessionOrderRequest.CustomerEmail = email;
        return await CreateAsync(createSessionOrderRequest, BookingType.Online);
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

        if (booking.IsCancelled)
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
        if (booking.BookingServices == null || !booking.BookingServices.Any() || booking.BookingServices.Any(bs => bs.Service == null))
        {
            throw new InvalidOperationException("Booking services not loaded correctly");
        }

        // Map services to response
        var bookingServiceResponses = booking.BookingServices.Select(service => new BookingServiceResponse
        {
            ServiceID = service.ServiceID,
            ServiceName = service.Service.ServiceName,
            Quantity = service.Quantity,
            Price = service.Price,
            SubTotal = service.SubTotal
        }).ToList();

        // Create the response
        var response = new BookingResponse
        {
            BookingID = booking.BookingID,
            ValidateCode = booking.ValidateCode,
            PaymentAmount = booking.PaymentAmount,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            BookingType = booking.BookingType,
            PaymentStatus = booking.PaymentStatus,
            Status = booking.Status,
            IsCancelled = booking.IsCancelled,
            CancelledDate = booking.CancelledDate,
            RefundAmount = booking.RefundAmount,
            CreatedDate = booking.CreatedDate,
            BoothID = booking.BoothID,
            CustomerID = booking.CustomerID,
            BookingServices = bookingServiceResponses
        };

        return response;
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
            })).ToList();
        foreach (var booking in bookings)
        {
            var list = await _bookingServiceRepository.GetAsync(i => i.BookingID == booking.BookingID, i => i.Service);
            booking.BookingServices = list.ToList();
        }
        return _mapper.Map<IEnumerable<BookingResponse>>(bookings.ToList());
    }

    public async Task<IEnumerable<BookingResponse>> GetAllPagingAsync(SessionOrderFilter filter, PagingModel paging)
    {
        var sessions = (await _bookingRepository.GetAsync(null, includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
            })).ToList().AutoFilter(filter);
        var listSessionresponse = _mapper.Map<IEnumerable<BookingResponse>>(sessions);
        return listSessionresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    // Get a session by ID
    public async Task<BookingResponse> GetByIdAsync(Guid id)
    {
        var booking = (await _bookingRepository.GetAsync(s => s.BookingID == id,
            includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
            })).FirstOrDefault();
        
        if (booking == null)
        {
            throw new KeyNotFoundException("Booking not found.");
        }
        var list = await _bookingServiceRepository.GetAsync(i => i.BookingID == booking.BookingID, i => i.Service);
        booking.BookingServices = list.ToList();
        return _mapper.Map<BookingResponse>(booking);
    }

    // Update a session
    public async Task UpdateAsync(Guid id, UpdateSessionOrderRequest updateModel)
    {
        //var session = (await _sessionOrderRepository.GetAsync(s => s.BookingID == id)).FirstOrDefault();
        //if (session == null)
        //{
        //    throw new KeyNotFoundException("Session not found.");
        //}
        //bool check = true;

        //DateTime startTime, endTime;
        //if (updateModel.StartTime.HasValue && default(DateTime) != updateModel.StartTime.Value)
        //{
        //    startTime = updateModel.StartTime.Value;
        //    TimeSpan duration = session.EndTime.Value - session.StartTime;
        //    endTime = startTime + duration;
        //}
        //else
        //{
        //    startTime = session.StartTime;
        //    endTime = session.EndTime.Value;
        //}
        //if (this.ValidateTimeRange(startTime, endTime) == false)
        //{
        //    throw new BadRequestException("Not valide time, please check our Branch open and close time");
        //}
        //if (updateModel.BoothID.Value != null && updateModel.BoothID.Value != default(Guid))
        //{
        //    check = await this.ValidateBookingTime(updateModel.BoothID.Value, startTime, endTime);
        //}
        //else
        //{
        //    check = await this.ValidateBookingTime(session.BoothID, startTime, endTime);
        //}
        //if (!check)
        //{
        //    throw new BadRequestException("There is another Session on this time, please check time to update again");
        //}
        //var updatedSession = _mapper.Map(updateModel, session);
        //await _sessionOrderRepository.UpdateAsync(updatedSession);
    }
    public async Task CancelSessionOrder(Guid sessionOrdeID, string? ipAddress)
    {
        var booking = (await _bookingRepository.GetAsync(i => i.BookingID == sessionOrdeID, i => i.FullPaymentPolicy)).FirstOrDefault();
        if (null == booking)
        {
            throw new NotFoundException("Session Order not found");
        } else if (booking.IsCancelled)
        {
            throw new BadRequestException("Booking already canceled");
        }
        else
        {
            var timeNow = DateTimeHelper.GetVietnamTimeNow();
            if (timeNow > booking.StartTime)
            {
                throw new BadRequestException("Can not cancel anymore, the session already start");
            }

            if (booking.Status == BookingStatus.Completed && (booking.StartTime.Date - timeNow).TotalDays > booking.FullPaymentPolicy.RefundDaysBefore)
            {
                //doing refund
                 await _refundService.RefundByBookingID(sessionOrdeID, false, ipAddress);
            }
            booking.IsCancelled = true;
            await _bookingRepository.UpdateAsync(booking);
        }
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
    private async Task ValidateBookingTime(Guid boothId, DateTime startTime, DateTime endTime)
    {
        var existingBookings = await _bookingRepository.GetAsync(i => i.BoothID == boothId
                                 && (startTime < i.StartTime && i.StartTime < endTime.AddMinutes(5) || endTime.AddMinutes(5) > i.EndTime && i.EndTime > startTime)
                                 && i.IsCancelled == false
                                 );
        if (existingBookings.Any())
        {
            throw new BadRequestException("There is another booking in the selected time range, please choose another time");
        }
    }
    private async Task<long> GenerateValidateCode()
    {
        long code = 0;
        var timeNow = DateTimeHelper.GetVietnamTimeNow();
        var existedCodes = (await _bookingRepository.GetAsync(i => i.IsCancelled == false && (i.StartTime > timeNow || i.EndTime >  timeNow))).ToList().Select(i => i.ValidateCode);
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
    private async Task<Account> ValidateCustomerAsync(string phoneNumber, string email)
    {
        Account? account;

        if (!string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(email))
        {
            account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(phoneNumber) && i.Email.Equals(email))).FirstOrDefault();
        }
        else if (!string.IsNullOrEmpty(phoneNumber))
        {
            account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(phoneNumber))).FirstOrDefault();
        }
        else if (!string.IsNullOrEmpty(email))
        {
            account = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).FirstOrDefault();
        }
        else
        {
            throw new BadRequestException("No customer value input");
        }
        if (account == null)
        {
            throw new BadRequestException("Account not found");
        }
        if (account.Role != AccountRole.Customer)
        {
            throw new BadRequestException("Account is not Customer");
        }
        if (account.Status != AccountStatus.Active)
        {
            throw new BadRequestException("Account is not active to do this function");
        }

        return account;
    }
    private async Task<Booth> ValidateBoothAsync(Guid boothID)
    {
        var booth = (await _boothRepository.GetAsync(i => i.BoothID == boothID, i => i.Branch)).FirstOrDefault();
        if (booth == null)
        {
            throw new NotFoundException("Booth not found on server, try again later");
        }
        else if (booth.isBooked || booth.Status == BoothStatus.Maintenance || booth.Status == BoothStatus.Inactive)
        {
            throw new BadRequestException("Booth is used by another or is inactive, in maintenance");
        }
        else if (booth.Branch.Status == BranchStatus.Inactive)
        {
            throw new BadRequestException("Branch of this booth has been closed, please try another branch");
        }

        return booth;
    }
    private async Task<(decimal, ICollection<Domain.Entities.BookingService>, decimal)> ProcessServiceListAsync(decimal boothPricePerHour, DateTime startTime, DateTime endTime, Dictionary<Guid, short> serviceList)
    {
       
        List<Domain.Entities.BookingService> result = new List<Domain.Entities.BookingService>();

        var totalHours = (endTime - startTime).TotalHours;
        var hireBoothFee = Math.Truncate((decimal)totalHours * boothPricePerHour);
        var bookingAmount = hireBoothFee;
        if (serviceList.Any())
        {
            var serviceIds = serviceList.Keys.ToList();
            var services = await _serviceRepository.GetAsync(s => serviceIds.Contains(s.ServiceID));
            if (serviceList.Count() != services.Count())
            {
                throw new BadRequestException("Service(s) from input not found.");
            }
            foreach (var service in services)
            {
                var subtotal = service.ServicePrice * serviceList[service.ServiceID];
                Domain.Entities.BookingService bookingService = new Domain.Entities.BookingService
                {
                    ServiceID = service.ServiceID,
                    Quantity = serviceList[service.ServiceID],
                    SubTotal = subtotal,
                    Price = service.ServicePrice,
                };
                result.Add(bookingService);
                bookingAmount += subtotal;
            }
        }

        return (bookingAmount, result, hireBoothFee);
    }

}

