using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq;
using System.Linq.Expressions;

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

    //Read
    public async Task<IQueryable<Session>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Sessions.Include(p => p.PrintPricing)
            .Include(d => d.Discount));
    }

    public async Task<IQueryable<Session>> GetAsync(Expression<Func<Session, bool>> predicate)
    {
        try
        {
            var result = _dbContext.Sessions.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<Session>().AsQueryable());
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
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

