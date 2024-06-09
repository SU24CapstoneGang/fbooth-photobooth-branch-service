using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Session;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.SessionServices;

public class SessionOrderService : ISessionOrderService
{
    private readonly ISessionOrderRepository _sessionRepository;
    private readonly IMapper _mapper;

    public SessionOrderService(ISessionOrderRepository sessionRepository, IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _mapper = mapper;
    }

    // Create a new session
    public async Task<Guid> CreateAsync(CreateSessionOrderRequest createModel)
    {
        var session = _mapper.Map<SessionOrder>(createModel);
        return await _sessionRepository.AddAsync(session);
    }

    // Delete a session by ID
    public async Task DeleteAsync(Guid id)
    {
        var session = (await _sessionRepository.GetAsync(s => s.SessionOrderID == id)).FirstOrDefault();
        if (session != null)
        {
            await _sessionRepository.RemoveAsync(session);
        }
        else
        {
            throw new KeyNotFoundException("Session not found.");
        }
    }

    // Get all sessions
    public async Task<IEnumerable<SessionOrderResponse>> GetAllAsync()
    {
        var sessions = await _sessionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions.ToList());
    }

    public async Task<IEnumerable<SessionOrderResponse>> GetAllPagingAsync(SessionOrderFilter filter, PagingModel paging)
    {
        var sessions = (await _sessionRepository.GetAllAsync()).ToList().AutoFilter(filter);
        var listSessionresponse = _mapper.Map<IEnumerable<SessionOrderResponse>>(sessions);
        listSessionresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        return listSessionresponse;
    }

    // Get a session by ID
    public async Task<SessionOrderResponse> GetByIdAsync(Guid id)
    {
        var session = (await _sessionRepository.GetAsync(s => s.SessionOrderID == id)).FirstOrDefault();
        if (session == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }
        return _mapper.Map<SessionOrderResponse>(session);
    }

    // Update a session
    public async Task UpdateAsync(Guid id, UpdateSessionOrderRequest updateModel)
    {
        var session = (await _sessionRepository.GetAsync(s => s.SessionOrderID == id)).FirstOrDefault();
        if (session == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }
        var updatedSession = _mapper.Map(updateModel, session);
        await _sessionRepository.UpdateAsync(updatedSession);
    }
}

