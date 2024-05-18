using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.SessionServices;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMapper _mapper;

    public SessionService(ISessionRepository sessionRepository, IMapper mapper)
    {
        _sessionRepository = sessionRepository;
        _mapper = mapper;
    }

    // Create a new session
    public async Task<Guid> CreateAsync(SessionDTO entityDTO)
    {
        var session = _mapper.Map<Session>(entityDTO);
        return await _sessionRepository.AddAsync(session);
    }

    // Delete a session by ID
    public async Task DeleteAsync(Guid id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
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
    public async Task<IEnumerable<SessionDTO>> GetAllAsync()
    {
        var sessions = await _sessionRepository.GetAll();
        return _mapper.Map<IEnumerable<SessionDTO>>(sessions);
    }

    // Get a session by ID
    public async Task<SessionDTO> GetByIdAsync(Guid id)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }
        return _mapper.Map<SessionDTO>(session);
    }

    // Update a session
    public async Task UpdateAsync(Guid id, SessionDTO entityDTO)
    {
        var session = await _sessionRepository.GetByIdAsync(id);
        if (session == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }

        var updatedSession = _mapper.Map(entityDTO, session);
        await _sessionRepository.UpdateAsync(updatedSession);
    }
}

