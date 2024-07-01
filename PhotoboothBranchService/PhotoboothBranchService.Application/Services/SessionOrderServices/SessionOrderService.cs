using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Application.Services.ServiceItemServices;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.SessionOrderServices;

public class SessionOrderService : ISessionOrderService
{
    private readonly ISessionOrderRepository _sessionOrderRepository;
    private readonly IMapper _mapper;
    private readonly IBoothRepository _boothRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IServiceItemService _serviceItemService;
    public SessionOrderService(ISessionOrderRepository sessionOrderRepository, IMapper mapper, IBoothRepository boothRepository, IPaymentRepository paymentRepository, IServiceItemService serviceItemService)
    {
        _sessionOrderRepository = sessionOrderRepository;
        _mapper = mapper;
        _boothRepository = boothRepository;
        _paymentRepository = paymentRepository;
        _serviceItemService = serviceItemService;
    }

    // Create a new session
    public async Task<CreateSessionOrderResponse> CreateAsync(CreateSessionOrderRequest createModel)
    {
        var boothTask = _boothRepository.GetAsync(i => i.BoothID == createModel.BoothID);
        var sessionOrderCheckTask = _sessionOrderRepository.GetAsync(i => i.AccountID == createModel.AccountID && (i.EndTime > DateTime.Now || !i.EndTime.HasValue));
        await Task.WhenAll(sessionOrderCheckTask, boothTask);

        //booth validate
        var booth = boothTask.Result.FirstOrDefault();
        if (booth == null)
        {
            throw new NotFoundException("Booth not found on server, try again later");
        }
        else if (booth.Status == ManufactureStatus.InUse || booth.Status == ManufactureStatus.Maintenance || booth.Status == ManufactureStatus.Inactive)
        {
            throw new Exception("Booth is used by another or is inactive, in maintenance");
        }

        //validate account's sesion order
        var sessionOrderCheck = sessionOrderCheckTask.Result.FirstOrDefault();
        if (sessionOrderCheck != null)
        {
            throw new Exception("This Account is using another booth");
        }


        //add session
        var session = _mapper.Map<SessionOrder>(createModel);
        session.ValidateCode = new Random().Next(100000, 1000000);
        await _sessionOrderRepository.AddAsync(session);
        try
        {
            await _serviceItemService.AddTheFirstServiceItem(session.SessionOrderID, createModel.ServiceID);
        } catch (Exception ex)
        {
            await _sessionOrderRepository.RemoveAsync(session);
            throw new Exception(ex.Message);
        }
        //update booth
        booth.Status = ManufactureStatus.InUse;
        await _boothRepository.UpdateAsync(booth);
        return _mapper.Map<CreateSessionOrderResponse>(session);
    }

    public async Task<SessionOrderResponse> ValidateSessionOrder(ValidateSessionOrderRequest validateSessionPhotoRequest)
    {
        var sessionOrder = (await _sessionOrderRepository
            .GetAsync(i => i.BoothID == validateSessionPhotoRequest.BoothID && i.StartTime < DateTime.Now && i.EndTime > DateTime.Now))
            .FirstOrDefault();
        if (sessionOrder != null)
        {
            if (sessionOrder.ValidateCode == validateSessionPhotoRequest.ValidateCode)
            {

                if (sessionOrder.Status == SessionOrderStatus.Waiting)
                {
                    TimeSpan difference = DateTime.Now - sessionOrder.StartTime;
                    sessionOrder.StartTime += difference;
                    sessionOrder.EndTime += difference;
                    sessionOrder.Status = SessionOrderStatus.Processsing;
                    await _sessionOrderRepository.UpdateAsync(sessionOrder);
                    return _mapper.Map<SessionOrderResponse>(sessionOrder);
                }
                else
                {
                    throw new Exception("Session has been cancelled");
                }
            }
            else
            {
                throw new Exception("Wrong validate code, please try again");
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
        var sessions = await _sessionOrderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions.ToList());
    }

    public async Task<IEnumerable<SessionOrderResponse>> GetAllPagingAsync(SessionOrderFilter filter, PagingModel paging)
    {
        var sessions = (await _sessionOrderRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listSessionresponse = _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions);
        return listSessionresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
    }

    // Get a session by ID
    public async Task<SessionOrderResponse> GetByIdAsync(Guid id)
    {
        var session = (await _sessionOrderRepository.GetAsync(s => s.SessionOrderID == id, i => i.ServiceItems)).FirstOrDefault();
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
        var updatedSession = _mapper.Map(updateModel, session);
        await _sessionOrderRepository.UpdateAsync(updatedSession);
    }
}

