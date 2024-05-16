using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _dbContext;

    public SessionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Add a new session
    public async Task<Guid> AddAsync(Session session)
    {
        _dbContext.Sessions.Add(session);
        await _dbContext.SaveChangesAsync();
        return session.SessionID;
    }

    // Get all sessions
    public async Task<IEnumerable<Session>> GetAll()
    {
        return await _dbContext.Sessions.ToListAsync();
    }

    // Get a session by ID
    public async Task<Session?> GetByIdAsync(Guid sessionId)
    {
        return await _dbContext.Sessions.FindAsync(sessionId);
    }

    // Remove a session
    public async Task RemoveAsync(Session session)
    {
        _dbContext.Sessions.Remove(session);
        await _dbContext.SaveChangesAsync();
    }

    // Update a session
    public async Task UpdateAsync(Session session)
    {
        _dbContext.Entry(session).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}

