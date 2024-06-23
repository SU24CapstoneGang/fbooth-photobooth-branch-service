using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class BackgroundRepository : IBackgroundRepository
{
    private readonly AppDbContext _dbContext;
    public BackgroundRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //Create
    public async Task<Background> AddAsync(Background Background)
    {
        var result = await _dbContext.AddAsync(Background);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    //Read
    public async Task<IQueryable<Background>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Backgrounds);
    }

    public async Task<IQueryable<Background>> GetAsync(
        Expression<Func<Background, bool>> predicate = null,
        params Expression<Func<Background, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.Backgrounds : _dbContext.Backgrounds.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(Enumerable.Empty<Background>().AsQueryable());
            }
            else
            {
                if (includeProperties != null)
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        if (IncludeHelper.IsValidInclude(includeProperty))
                        {
                            result = result.Include(includeProperty);
                        }
                    }
                }
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    //Delete
    public async Task RemoveAsync(Background Background)
    {
        _dbContext.Remove(Background);
        await _dbContext.SaveChangesAsync();
    }

    //Update
    public async Task UpdateAsync(Background Background)
    {
        Background.LastModified = DateTime.UtcNow;
        _dbContext.Update(Background);
        await _dbContext.SaveChangesAsync();
    }
}
