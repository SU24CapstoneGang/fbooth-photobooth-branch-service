using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class BoothBranchRepository : IBoothBranchRepository
{
    private readonly AppDbContext _dbContext;

    public BoothBranchRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Guid> AddAsync(BoothBranch photoBoothBranch)
    {
        await _dbContext.AddAsync(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
        return photoBoothBranch.BoothBranchID;
    }

    //Read
    public async Task<IQueryable<BoothBranch>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.BoothBranches);
    }

    public async Task<IQueryable<BoothBranch>> GetAsync(
        Expression<Func<BoothBranch, bool>> predicate = null,
        params Expression<Func<BoothBranch, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.BoothBranches : _dbContext.BoothBranches.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(Enumerable.Empty<BoothBranch>().AsQueryable());
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

    //Update
    public async Task UpdateAsync(BoothBranch photoBoothBranch)
    {
        _dbContext.Update(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
    }

    //Delete
    public async Task RemoveAsync(BoothBranch photoBoothBranch)
    {
        _dbContext.Remove(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
    }
}

