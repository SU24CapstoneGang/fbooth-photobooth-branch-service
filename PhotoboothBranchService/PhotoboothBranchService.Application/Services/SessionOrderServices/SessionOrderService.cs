﻿using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Application.Services.PaymentServices;
using PhotoboothBranchService.Application.Services.RefundServices;
using PhotoboothBranchService.Application.Services.ServiceItemServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Application.Services.SessionOrderServices;

public class SessionOrderService : ISessionOrderService
{
    private readonly ISessionOrderRepository _sessionOrderRepository;
    private readonly IMapper _mapper;
    private readonly IBoothRepository _boothRepository;
    private readonly IPaymentService _paymentService;
    private readonly IServiceItemService _serviceItemService;
    private readonly ISessionPackageRepository _sessionPackageRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRefundService _refundService;
    public SessionOrderService(ISessionOrderRepository sessionOrderRepository,
        IMapper mapper,
        IBoothRepository boothRepository,
        IPaymentService paymentService,
        IServiceItemService serviceItemService,
        ISessionPackageRepository sessionPackageRepository,
        IServiceRepository serviceRepository,
        IAccountRepository accountRepository,
        IRefundService refundService)
    {
        _sessionOrderRepository = sessionOrderRepository;
        _mapper = mapper;
        _boothRepository = boothRepository;
        _paymentService = paymentService;
        _serviceItemService = serviceItemService;
        _sessionPackageRepository = sessionPackageRepository;
        _serviceRepository = serviceRepository;
        _accountRepository = accountRepository;
        _refundService = refundService;
    }

    // Create a new session
    public async Task<CreateSessionOrderResponse> CreateAsync(CreateSessionOrderRequest createModel)
    {
        //validate account
        Account? account;

        if (!string.IsNullOrEmpty(createModel.CustomerPhoneNumber) && !string.IsNullOrEmpty(createModel.CustomerEmail))
        {
            account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(createModel.CustomerPhoneNumber) && i.Email.Equals(createModel.CustomerEmail) && i.Status == AccountStatus.Active)).FirstOrDefault();
        }
        else if (!string.IsNullOrEmpty(createModel.CustomerPhoneNumber))
        {
            account = (await _accountRepository.GetAsync(i => i.PhoneNumber.Equals(createModel.CustomerPhoneNumber) && i.Status == AccountStatus.Active)).FirstOrDefault();
        }
        else if (!string.IsNullOrEmpty(createModel.CustomerEmail))
        {
            account = (await _accountRepository.GetAsync(i => i.Email.Equals(createModel.CustomerEmail) && i.Status == AccountStatus.Active)).FirstOrDefault();
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

        var boothTask = _boothRepository.GetAsync(i => i.BoothID == createModel.BoothID);
        var sessionPackageTask = _sessionPackageRepository.GetAsync(i => i.SessionPackageID == createModel.SessionPackageID);
        var sessionOrderCheckTask = _sessionOrderRepository.GetAsync(i => i.AccountID == account.AccountID && (i.EndTime > DateTime.Now && DateTime.Now > i.StartTime));
        await Task.WhenAll(sessionOrderCheckTask, sessionPackageTask, boothTask);

        //booth validate
        var booth = boothTask.Result.FirstOrDefault();
        if (booth == null)
        {
            throw new NotFoundException("Booth not found on server, try again later");
        }
        else if (booth.Status == ManufactureStatus.InUse || booth.Status == ManufactureStatus.Maintenance || booth.Status == ManufactureStatus.Inactive)
        {
            throw new BadRequestException("Booth is used by another or is inactive, in maintenance");
        }

        //validate account's sesion order
        var sessionOrderCheck = sessionOrderCheckTask.Result.FirstOrDefault();
        if (sessionOrderCheck != null)
        {
            throw new BadRequestException("This Account is using another booth in booking time");
        }

        var sessionPackage = sessionPackageTask.Result.FirstOrDefault();
        if (sessionPackage == null)
        {
            throw new NotFoundException("Session Package Not found");
        }

        //validate service list of create model
        if (createModel.ServiceList.Count > 0)
        {
            var serviceIds = createModel.ServiceList.Keys.ToList();
            var services = await _serviceRepository.GetAsync(i => serviceIds.Contains(i.ServiceID));
            if (createModel.ServiceList.Count != services.Count())
            {
                throw new BadRequestException("Some service in request are not found");
            }
        }

        //add session with package
        SessionOrder session = _mapper.Map<SessionOrder>(createModel);
        session.AccountID = account.AccountID;
        session.ValidateCode = await this.GenerateValidateCode();
        if (createModel.StartTime == default(DateTime))
        {
            session.StartTime = DateTime.Now;
            //update booth
            booth.Status = ManufactureStatus.InUse;
        }
        else if (createModel.StartTime < DateTime.Now)
        {
            throw new BadRequestException("Can not booking with start time in past");
        }

        session.EndTime = session.StartTime.AddMinutes(sessionPackage.Duration);
        //validate time in a date and in branch's open time
        if (!ValidateTimeRange(session.StartTime, session.EndTime.Value))
        {
            throw new BadRequestException("Not valide time, our Branch open from 8:00 to 23:00 of a day");
        }

        session.TotalPrice = sessionPackage.Price;
        session.Status = SessionOrderStatus.Created;
        //validate time to not conflict with other session
        if ((await this.ValidateBookingTime(session.BoothID, session.StartTime, session.EndTime.Value)) == false)
        {
            throw new BadRequestException("There is another Session on this time, please check time to book again");
        }

        if (booth.Status == ManufactureStatus.InUse)
        {
            await _boothRepository.UpdateAsync(booth);
        }
        await _sessionOrderRepository.AddAsync(session);

        if (createModel.ServiceList.Count > 0)
        {
            foreach (var service in createModel.ServiceList)
            {
                CreateServiceItemRequest serviceItem = new CreateServiceItemRequest
                {
                    Quantity = service.Value,
                    ServiceID = service.Key,
                    SessionOrderID = session.SessionOrderID,
                };
                await _serviceItemService.CreateAsync(serviceItem);
            }
        }

        return _mapper.Map<CreateSessionOrderResponse>(session);
    }

    public async Task<CreateSessionOrderResponse> CustomerBooking(CustomerBookingSessionOrderRequest request, string email)
    {
        if ((request.StartTime - DateTime.Now).TotalMinutes <= 30)
        {
            throw new BadRequestException("You must booking a session with start time at least 30 minutes from now");
        }
        var createSessionOrderRequest = _mapper.Map<CreateSessionOrderRequest>(request);
        createSessionOrderRequest.CustomerEmail = email;
        return await this.CreateAsync(createSessionOrderRequest);
    }

    public async Task<SessionOrderResponse> ValidateSessionOrder(ValidateSessionOrderRequest validateSessionPhotoRequest)
    {
        var sessionOrders = (await _sessionOrderRepository
            .GetAsync(i => i.BoothID == validateSessionPhotoRequest.BoothID && i.EndTime > DateTime.Now,
            includeProperties: new Expression<Func<SessionOrder, object>>[]
            {
                i => i.ServiceItems,
                i => i.SessionPackage
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

            if (sessionOrder.Status == SessionOrderStatus.Waiting)
            {
                TimeSpan difference = DateTime.Now - sessionOrder.StartTime;
                if (difference.TotalMinutes < 5)
                {
                    sessionOrder.StartTime += difference;
                    sessionOrder.EndTime += difference;
                }
                else
                {
                    sessionOrder.StartTime += difference;
                }
                sessionOrder.Status = SessionOrderStatus.Processsing;
                await _sessionOrderRepository.UpdateAsync(sessionOrder);

                //update booth
                booth.Status = ManufactureStatus.InUse;
                await _boothRepository.UpdateAsync(booth);
                return _mapper.Map<SessionOrderResponse>(sessionOrder);
            }
            else
            {
                throw new BadRequestException("Session has been cancelled or not paid yet to validate");
            }
        }
        else
        {
            throw new NotFoundException("No session order found, please resigter session with our staff");
        }
    }
    // Delete a session by ID
    public async Task DeleteAsync(Guid id)
    {
        var session = (await _sessionOrderRepository.GetAsync(s => s.SessionOrderID == id)).FirstOrDefault();
        if (session != null)
        {
            await _sessionOrderRepository.RemoveAsync(session);
        }
        else
        {
            throw new KeyNotFoundException("Session not found.");
        }
    }

    // Get all sessions
    public async Task<IEnumerable<SessionOrderResponse>> GetAllAsync()
    {
        var sessions = await _sessionOrderRepository.GetAsync(null, includeProperties: new Expression<Func<SessionOrder, object>>[]
            {
                i => i.ServiceItems,
                i => i.SessionPackage
            });
        return _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions.ToList());
    }

    public async Task<IEnumerable<SessionOrderResponse>> GetAllPagingAsync(SessionOrderFilter filter, PagingModel paging)
    {
        var sessions = (await _sessionOrderRepository.GetAsync(null, includeProperties: new Expression<Func<SessionOrder, object>>[]
            {
                i => i.ServiceItems,
                i => i.SessionPackage
            })).ToList().AutoFilter(filter);
        var listSessionresponse = _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions);
        return listSessionresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    // Get a session by ID
    public async Task<SessionOrderResponse> GetByIdAsync(Guid id)
    {
        var session = (await _sessionOrderRepository.GetAsync(s => s.SessionOrderID == id,
            includeProperties: new Expression<Func<SessionOrder, object>>[]
            {
                i => i.ServiceItems,
                i => i.SessionPackage
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
        var session = (await _sessionOrderRepository.GetAsync(s => s.SessionOrderID == id)).FirstOrDefault();
        if (session == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }
        bool check = true;

        DateTime startTime, endTime;
        if (updateModel.StartTime.HasValue && default(DateTime) != updateModel.StartTime.Value)
        {
            startTime = updateModel.StartTime.Value;
            TimeSpan duration = session.EndTime.Value - session.StartTime;
            endTime = startTime + duration;
        }
        else
        {
            startTime = session.StartTime;
            endTime = session.EndTime.Value;
        }
        if (this.ValidateTimeRange(startTime, endTime) == false)
        {
            throw new BadRequestException("Not valide time, our Branch open from 8:00 to 23:00 of a day");
        }
        if (updateModel.BoothID.Value != null && updateModel.BoothID.Value != default(Guid))
        {
            check = await this.ValidateBookingTime(updateModel.BoothID.Value, startTime, endTime);
        }
        else
        {
            check = await this.ValidateBookingTime(session.BoothID, startTime, endTime);
        }
        if (!check)
        {
            throw new BadRequestException("There is another Session on this time, please check time to update again");
        }
        var updatedSession = _mapper.Map(updateModel, session);
        await _sessionOrderRepository.UpdateAsync(updatedSession);
    }
    public async Task CancelSessionOrder(Guid sessionOrdeID, string? ipAddress)
    {
        var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.SessionOrderID == sessionOrdeID)).FirstOrDefault();
        if (null == sessionOrder)
        {
            throw new NotFoundException("Session Order not found");
        }
        else
        {
            if (DateTime.Now > sessionOrder.StartTime
                && (sessionOrder.Status == SessionOrderStatus.Waiting
                    || sessionOrder.Status == SessionOrderStatus.Created
                    || sessionOrder.Status == SessionOrderStatus.Deposited))
            {
                throw new BadRequestException("Can not cancel anymore, the session already start");
            }

            if (sessionOrder.Status == SessionOrderStatus.Waiting)
            {
                //doing refund
                await _refundService.RefundByOrderId(sessionOrdeID, false, ipAddress);
            }
            sessionOrder.Status = SessionOrderStatus.Canceled;
            await _sessionOrderRepository.UpdateAsync(sessionOrder);
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
        var validateTime = (await _sessionOrderRepository.GetAsync(i => i.BoothID == boothId
                                 && ((startTime < i.StartTime && i.StartTime < endTime.AddMinutes(5)) || (endTime.AddMinutes(5) > i.EndTime && i.EndTime > startTime))
                                 && (i.Status != SessionOrderStatus.Done && i.Status == SessionOrderStatus.Canceled)
                                 )).FirstOrDefault();
        return validateTime == null;
    }
    private async Task<long> GenerateValidateCode()
    {
        long code = 0;
        var existedCodes = (await _sessionOrderRepository.GetAsync(i => i.Status != SessionOrderStatus.Canceled || i.Status != SessionOrderStatus.Done)).ToList().Select(i => i.ValidateCode);
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

