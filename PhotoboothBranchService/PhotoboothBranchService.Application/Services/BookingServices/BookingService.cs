using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booking;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Application.Services.BookingServiceServices;
using PhotoboothBranchService.Application.Services.PaymentServices;
using PhotoboothBranchService.Application.Services.RefundServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Globalization;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Application.Services.BookingServices;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;
    private readonly IBoothRepository _boothRepository;
    private readonly ITransactionService _paymentService;
    private readonly IBookingServiceService _bookingServiceService;
    private readonly IAccountRepository _accountRepository;
    private readonly IRefundService _refundService;
    public BookingService(IBookingRepository sessionOrderRepository,
        IMapper mapper,
        IBoothRepository boothRepository,
        ITransactionService paymentService,
        IBookingServiceService servicePackageService,
        IAccountRepository accountRepository,
        IRefundService refundService)
    {
        _bookingRepository = sessionOrderRepository;
        _mapper = mapper;
        _boothRepository = boothRepository;
        _paymentService = paymentService;
        _bookingServiceService = servicePackageService;
        _accountRepository = accountRepository;
        _refundService = refundService;
    }

    // Create a new session
    public async Task<CreateBookingResponse> CreateAsync(BookingRequest createModel)
    {
        ////validate account
        //Account? account;

        //if (!string.IsNullOrEmpty(createModel.CustomerPhoneNumber) && !string.IsNullOrEmpty(createModel.CustomerEmail))
        //{
        //    account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(createModel.CustomerPhoneNumber) && i.Email.Equals(createModel.CustomerEmail))).FirstOrDefault();
        //}
        //else if (!string.IsNullOrEmpty(createModel.CustomerPhoneNumber))
        //{
        //    account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(createModel.CustomerPhoneNumber))).FirstOrDefault();
        //}
        //else if (!string.IsNullOrEmpty(createModel.CustomerEmail))
        //{
        //    account = (await _accountRepository.GetAsync(i => i.Email.Equals(createModel.CustomerEmail))).FirstOrDefault();
        //}
        //else
        //{
        //    throw new BadRequestException("No customer value input");
        //}
        //if (account == null)
        //{
        //    throw new BadRequestException("Account not found");
        //}
        //if (account.Role != AccountRole.Customer)
        //{
        //    throw new BadRequestException("Account is not Customer");
        //}
        //if (account.Status != AccountStatus.Active)
        //{
        //    throw new BadRequestException("Account is not active to do this function");
        //}

        //var boothTask = _boothRepository.GetAsync(i => i.BoothID == createModel.BoothID, i => i.Branch);
        //var sessionOrderCheckTask = _sessionOrderRepository.GetAsync(i => i.CustomerID == account.AccountID && (i.EndTime > DateTime.Now && DateTime.Now > i.StartTime));
        //await Task.WhenAll(sessionOrderCheckTask, boothTask);

        ////booth validate
        //var booth = boothTask.Result.FirstOrDefault();
        //if (booth == null)
        //{
        //    throw new NotFoundException("Booth not found on server, try again later");
        //}
        //else if (booth.Status == BoothStatus.InUse || booth.Status == BoothStatus.Maintenance || booth.Status == BoothStatus.Inactive)
        //{
        //    throw new BadRequestException("Booth is used by another or is inactive, in maintenance");
        //} else if (booth.Branch.Status == BranchStatus.Inactive)
        //{
        //    throw new BadRequestException("Branch of this booth has been closed, plase try another branch");
        //}

        ////validate account's sesion order
        //var sessionOrderCheck = sessionOrderCheckTask.Result.FirstOrDefault();
        //if (sessionOrderCheck != null)
        //{
        //    throw new BadRequestException("This Account is using another booth in booking time");
        //}

        ////add session with package
        //Booking session = _mapper.Map<Booking>(createModel);
        //session.CustomerID = account.AccountID;
        //session.ValidateCode = await this.GenerateValidateCode();
        //if (createModel.StartTime == default(DateTime))
        //{
        //    session.StartTime = DateTime.Now;
        //    //update booth
        //    booth.Status = BoothStatus.InUse;
        //}
        //else if (createModel.StartTime < DateTime.Now)
        //{
        //    throw new BadRequestException("Can not booking with start time in past");
        //}
        ////validate service list of create model
        //if (createModel.ServiceList.Count > 0)
        //{
        //    var serviceIds = createModel.ServiceList.Keys.ToList();
        //    var services = await _serviceRepository.GetAsync(i => serviceIds.Contains(i.ServicePackageID));
        //    //validate number of service
        //    if (createModel.ServiceList.Count != services.Count())
        //    {
        //        throw new BadRequestException("Some service in request are not found");
        //    }
        //    //validate is there any service about hire booth and update endtime
        //    session.EndTime = session.StartTime;
        //}
        ////validate time in a date and in branch's open time
        //if (!ValidateTimeRange(session.StartTime, session.EndTime.Value))
        //{
        //    throw new BadRequestException("Not valide time, our Branch open from 8:00 to 23:00 of a day");
        //}

        //session.Status = SessionOrderStatus.Created;
        ////validate time to not conflict with other session
        //if ((await this.ValidateBookingTime(session.BoothID, session.StartTime, session.EndTime.Value)) == false)
        //{
        //    throw new BadRequestException("There is another Session on this time, please check time to book again");
        //}

        //if (booth.Status == BoothStatus.InUse)
        //{
        //    await _boothRepository.UpdateAsync(booth);
        //}
        //await _sessionOrderRepository.AddAsync(session);

        //if (createModel.ServiceList.Count > 0)
        //{
        //    foreach (var service in createModel.ServiceList)
        //    {
        //        CreateServiceItemRequest serviceItem = new CreateServiceItemRequest
        //        {
        //            Quantity = service.Value,
        //            ServiceID = service.Key,
        //            SessionOrderID = session.BookingID,
        //        };
        //        await _serviceItemService.CreateAsync(serviceItem);
        //    }
        //}

        //return _mapper.Map<CreateSessionOrderResponse>(session);r
        return null;
    }



    //public async Task<CreateBookingResponse> CreateAsync(CreateBookingRequest createModel)
    //{
    //    // Validate customer
    //    var account = await ValidateCustomerAsync(createModel.CustomerPhoneNumber, createModel.CustomerEmail);

    //    // Validate booth
    //    var booth = await ValidateBoothAsync(createModel.BoothID);

    //    // Validate time range
    //    ValidateTimeRange(createModel.StartTime, createModel.EndTime);

    //    // Check if booking time is within business hours
    //    if (!IsWithinBusinessHours(createModel.StartTime) || !IsWithinBusinessHours(createModel.EndTime))
    //    {
    //        throw new BadRequestException("Booking time must be within business hours.");
    //    }

    //    // Validate booking time conflicts
    //    await ValidateBookingTimeAsync(createModel.BoothID, createModel.StartTime, createModel.EndTime);

    //    // Calculate payment amount
    //    var paymentAmount = await CalculatePaymentAmount(createModel.BoothID, createModel.StartTime, createModel.EndTime, createModel.ServiceList);

    //    // Map to booking entity
    //    var booking = _mapper.Map<Booking>(createModel);
    //    booking.CustomerID = account.AccountID;
    //    booking.ValidateCode = await GenerateValidateCode();
    //    booking.Status = BookingStatus.Created;
    //    booking.IsCancelled = false;
    //    booking.CreatedDate = DateTime.UtcNow;
    //    booking.PaymentAmount = paymentAmount;

    //    // Set payment policy if applicable
    //    if (createModel.BookingType == BookingType.Online)
    //    {
    //        booking.FullPaymentPolicyID = await GetApplicablePolicyIdAsync(createModel.StartTime);
    //    }

    //    // Process service list
    //    await ProcessServiceListAsync(createModel.ServiceList, booking);

    //    // Save booking
    //    await _bookingRepository.AddAsync(booking);

    //    return _mapper.Map<CreateBookingResponse>(booking);
    //}

    //private async Task<Account> ValidateCustomerAsync(string phoneNumber, string email)
    //{
    //    Account? account;

    //    if (!string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(email))
    //    {
    //        account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(phoneNumber) && i.Email.Equals(email))).FirstOrDefault();
    //    }
    //    else if (!string.IsNullOrEmpty(phoneNumber))
    //    {
    //        account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(phoneNumber))).FirstOrDefault();
    //    }
    //    else if (!string.IsNullOrEmpty(email))
    //    {
    //        account = (await _accountRepository.GetAsync(i => i.Email.Equals(email))).FirstOrDefault();
    //    }
    //    else
    //    {
    //        throw new BadRequestException("No customer value input");
    //    }
    //    if (account == null)
    //    {
    //        throw new BadRequestException("Account not found");
    //    }
    //    if (account.Role != AccountRole.Customer)
    //    {
    //        throw new BadRequestException("Account is not Customer");
    //    }
    //    if (account.Status != AccountStatus.Active)
    //    {
    //        throw new BadRequestException("Account is not active to do this function");
    //    }

    //    return account;
    //}

    //private async Task<Booth> ValidateBoothAsync(Guid boothID)
    //{
    //    var booth = (await _boothRepository.GetAsync(i => i.BoothID == boothID, i => i.Branch)).FirstOrDefault();
    //    if (booth == null)
    //    {
    //        throw new NotFoundException("Booth not found on server, try again later");
    //    }
    //    else if (booth.Status == BoothStatus.InUse || booth.Status == BoothStatus.Maintenance || booth.Status == BoothStatus.Inactive)
    //    {
    //        throw new BadRequestException("Booth is used by another or is inactive, in maintenance");
    //    }
    //    else if (booth.Branch.Status == BranchStatus.Inactive)
    //    {
    //        throw new BadRequestException("Branch of this booth has been closed, please try another branch");
    //    }

    //    return booth;
    //}

    //private void ValidateTimeRange(DateTime startTime, DateTime endTime)
    //{
    //    if (startTime >= endTime)
    //    {
    //        throw new BadRequestException("Start time must be before end time");
    //    }

    //    if (startTime < DateTime.Now)
    //    {
    //        throw new BadRequestException("Start time cannot be in the past");
    //    }
    //}

    //private bool IsWithinBusinessHours(DateTime time)
    //{
    //    var openTime = new TimeSpan(8, 0, 0); // 8:00 AM
    //    var closeTime = new TimeSpan(23, 0, 0); // 11:00 PM
    //    var timeOfDay = time.TimeOfDay;

    //    return timeOfDay >= openTime && timeOfDay <= closeTime;
    //}

    //private async Task ValidateBookingTimeAsync(Guid boothID, DateTime startTime, DateTime endTime)
    //{
    //    var existingBookings = await _bookingRepository.GetAsync(b => b.BoothID == boothID &&
    //        ((b.StartTime <= startTime && b.EndTime > startTime) ||
    //        (b.StartTime < endTime && b.EndTime >= endTime) ||
    //        (b.StartTime >= startTime && b.EndTime <= endTime)));

    //    if (existingBookings.Any())
    //    {
    //        throw new BadRequestException("There is another booking in the selected time range, please choose another time");
    //    }
    //}
    //private async Task<decimal> CalculatePaymentAmount(Guid boothID, DateTime startTime, DateTime endTime, Dictionary<Guid, int> serviceList)
    //{
    //    var booth = (await _boothRepository.GetAsync(b => b.BoothID == boothID)).FirstOrDefault();
    //    if (booth == null)
    //    {
    //        throw new NotFoundException("Booth not found.");
    //    }

    //    var boothPricePerHour = booth.PricePerHour;
    //    var totalHours = (endTime - startTime).TotalHours;
    //    var boothAmount = (decimal)totalHours * boothPricePerHour;

    //    var serviceAmount = 0m;
    //    if (serviceList.Any())
    //    {
    //        var serviceIds = serviceList.Keys.ToList();
    //        var services = await _serviceRepository.GetAsync(s => serviceIds.Contains(s.ServicePackageID));
    //        foreach (var service in services)
    //        {
    //            serviceAmount += service.Price * serviceList[service.ServicePackageID];
    //        }
    //    }

    //    return boothAmount + serviceAmount;
    //}

    //private async Task ProcessServiceListAsync(Dictionary<Guid, int> serviceList, Booking booking)
    //{
    //    if (serviceList.Count > 0)
    //    {
    //        foreach (var service in serviceList)
    //        {
    //            BookingService bookingService = new BookingService
    //            {
    //                BookingID = booking.BookingID,
    //                ServicePackageID = service.Key,
    //                Quantity = service.Value
    //            };
    //            // Save booking service to the database
    //            await _serviceRepository.AddBookingServiceAsync(bookingService);
    //        }
    //    }
    //}

    //private async Task<long> GenerateValidateCode()
    //{
    //    var random = new Random();
    //    return random.Next(100000, 999999);
    //}

    //private async Task<Guid?> GetApplicablePolicyIdAsync(DateTime startTime)
    //{
    //    var policies = await _fullPaymentPolicyRepository.GetAsync(p => p.IsActive && (p.StartDate == null || p.StartDate <= startTime) && (p.EndDate == null || p.EndDate >= startTime));
    //    var policy = policies.FirstOrDefault();
    //    return policy?.FullPaymentPolicyID;
    //}

    public async Task<CreateBookingResponse> CustomerBooking(CustomerBookingSessionOrderRequest request, string email)
    {
        if ((request.StartTime - DateTime.Now).TotalMinutes <= 30)
        {
            throw new BadRequestException("You must booking a session with start time at least 30 minutes from now");
        }
        var createSessionOrderRequest = _mapper.Map<BookingRequest>(request);
        createSessionOrderRequest.CustomerEmail = email;
        return await CreateAsync(createSessionOrderRequest);
    }

    public async Task<SessionOrderResponse> ValidateBookingService(ValidateSessionOrderRequest validateSessionPhotoRequest)
    {
        var sessionOrders = (await _bookingRepository
            .GetAsync(i => i.BoothID == validateSessionPhotoRequest.BoothID && i.EndTime > DateTime.Now,
            includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
            })).ToList();
        var booth = (await _boothRepository.GetAsync(i => i.BoothID == validateSessionPhotoRequest.BoothID)).FirstOrDefault();
        if (sessionOrders.Count() == 0 && booth != null)
        {
            var sessionOrder = sessionOrders.FirstOrDefault(i => i.ValidateCode == validateSessionPhotoRequest.ValidateCode);
            if (sessionOrder == null)
            {
                throw new BadRequestException("Wrong validate code, please try again");
            }
            if (sessionOrder.StartTime > DateTime.Now)
            {
                throw new BadRequestException("The time for your Session not come yet, please check with our staff and try again later");
            }

            //if (sessionOrder.Status == BookingStatus.Waiting)
            //{
            //    TimeSpan difference = DateTime.Now - sessionOrder.StartTime;
            //    if (difference.TotalMinutes < 5)
            //    {
            //        sessionOrder.StartTime += difference;
            //        sessionOrder.EndTime += difference;
            //    }
            //    else
            //    {
            //        sessionOrder.StartTime += difference;
            //    }
            //    sessionOrder.Status = BookingStatus.Processsing;
            //    await _bookingRepository.UpdateAsync(sessionOrder);

            //    //update booth
            //    booth.isBooked = true;
            //    await _boothRepository.UpdateAsync(booth);
                return _mapper.Map<SessionOrderResponse>(sessionOrder);
            //}
            //else
            //{
            //    throw new BadRequestException("Session has been cancelled or not paid yet to validate");
            //}
        }
        else
        {
            throw new NotFoundException("No session order found, please resigter session with our staff");
        }
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
    public async Task<IEnumerable<SessionOrderResponse>> GetAllAsync()
    {
        var sessions = await _bookingRepository.GetAsync(null, includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
            });
        return _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions.ToList());
    }

    public async Task<IEnumerable<SessionOrderResponse>> GetAllPagingAsync(SessionOrderFilter filter, PagingModel paging)
    {
        var sessions = (await _bookingRepository.GetAsync(null, includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
            })).ToList().AutoFilter(filter);
        var listSessionresponse = _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions);
        return listSessionresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    // Get a session by ID
    public async Task<SessionOrderResponse> GetByIdAsync(Guid id)
    {
        var session = (await _bookingRepository.GetAsync(s => s.BookingID == id,
            includeProperties: new Expression<Func<Booking, object>>[]
            {
                i => i.BookingServices,
            })).FirstOrDefault();
        if (session == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }
        return _mapper.Map<SessionOrderResponse>(session);
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
        var sessionOrder = (await _bookingRepository.GetAsync(i => i.BookingID == sessionOrdeID)).FirstOrDefault();
        if (null == sessionOrder)
        {
            throw new NotFoundException("Session Order not found");
        }
        else
        {
            //if (DateTime.Now > sessionOrder.StartTime
            //    && (sessionOrder.Status == BookingStatus.Waiting
            //        || sessionOrder.Status == BookingStatus.Created
            //        || sessionOrder.Status == BookingStatus.Deposited))
            //{
            //    throw new BadRequestException("Can not cancel anymore, the session already start");
            //}

            //if (sessionOrder.Status == BookingStatus.Waiting)
            //{
            //    //doing refund
            //    await _refundService.RefundByOrderId(sessionOrdeID, false, ipAddress);
            //}
            sessionOrder.Status = BookingStatus.Canceled;
            await _bookingRepository.UpdateAsync(sessionOrder);
        }
    }
    private bool ValidateTimeRange(DateTime startTime, DateTime endTime)
    {

        DateTime lowerBound = new DateTime(startTime.Year, startTime.Month, startTime.Day, 8, 0, 0); // 8:00 AM
        DateTime upperBound = new DateTime(startTime.Year, startTime.Month, startTime.Day, 23, 0, 0); // 11:00 PM

        bool isSameDate = startTime.Date == endTime.Date;
        bool isStartTimeValid = startTime >= lowerBound && startTime <= upperBound;
        bool isEndTimeValid = endTime >= lowerBound && endTime <= upperBound;

        return isSameDate && isStartTimeValid && isEndTimeValid;
    }
    private async Task<bool> ValidateBookingTime(Guid boothId, DateTime startTime, DateTime endTime)
    {
        var validateTime = (await _bookingRepository.GetAsync(i => i.BoothID == boothId
                                 && (startTime < i.StartTime && i.StartTime < endTime.AddMinutes(5) || endTime.AddMinutes(5) > i.EndTime && i.EndTime > startTime)
                                 && i.Status != BookingStatus.Canceled
                                 )).FirstOrDefault();
        return validateTime == null;
    }
    private async Task<long> GenerateValidateCode()
    {
        long code = 0;
        var existedCodes = (await _bookingRepository.GetAsync(i => i.Status != BookingStatus.Canceled || i.Status != BookingStatus.Completed)).ToList().Select(i => i.ValidateCode);
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
}

