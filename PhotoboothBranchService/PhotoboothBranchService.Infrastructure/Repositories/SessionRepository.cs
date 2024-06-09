using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
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
    public async Task<Guid> AddAsync(SessionOrder session)
    {
        _dbContext.SessionOrders.Add(session);
        await _dbContext.SaveChangesAsync();
        return session.SessionOrderID;
    }

    //Read
    public async Task<IQueryable<SessionOrder>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.SessionOrders);
    }

    public async Task<IQueryable<SessionOrder>> GetAsync(Expression<Func<SessionOrder, bool>> predicate)
    {
        try
        {
            var result = _dbContext.SessionOrders.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<SessionOrder>().AsQueryable());
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }

    // Remove a session
    public async Task RemoveAsync(SessionOrder session)
    {
        _dbContext.SessionOrders.Remove(session);
        await _dbContext.SaveChangesAsync();
    }

    // Update a session
    public async Task UpdateAsync(SessionOrder session)
    {
        _dbContext.Entry(session).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}

