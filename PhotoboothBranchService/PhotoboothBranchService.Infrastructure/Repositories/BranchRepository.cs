using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly AppDbContext _dbContext;

    public BranchRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Branch> AddAsync(Branch photoBoothBranch)
    {
        var result = await _dbContext.AddAsync(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    //Read
    public async Task<IQueryable<Branch>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Branches);
    }

    public async Task<IQueryable<Branch>> GetAsync(
        Expression<Func<Branch, bool>> predicate = null,
        params Expression<Func<Branch, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.Branches : _dbContext.Branches.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(Enumerable.Empty<Branch>().AsQueryable());
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
    public async Task UpdateAsync(Branch photoBoothBranch)
    {
        _dbContext.Update(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
    }

    //Delete
    public async Task RemoveAsync(Branch photoBoothBranch)
    {
        _dbContext.Remove(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
    }
}

