using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class LayoutRepository : ILayoutRepository
{
    private readonly AppDbContext _dbContext;

    public LayoutRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //Create
    public async Task<Guid> AddAsync(Layout layout)
    {
        await _dbContext.AddAsync(layout);
        await _dbContext.SaveChangesAsync();
        return layout.LayoutID;
    }
    //Read
    public async Task<IQueryable<Layout>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Layouts);
    }

    public async Task<IQueryable<Layout>> GetAsync(
        Expression<Func<Layout, bool>> predicate = null,
        params Expression<Func<Layout, object>>[] includeProperties)
    {
        try
        {
            var result = predicate == null ? _dbContext.Layouts : _dbContext.Layouts.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(Enumerable.Empty<Layout>().AsQueryable());
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
    public async Task RemoveAsync(Layout layout)
    {
        _dbContext.Remove(layout);
        await _dbContext.SaveChangesAsync();
    }
    //Update
    public async Task UpdateAsync(Layout layout)
    {
        layout.LastModified = DateTime.UtcNow;
        _dbContext.Update(layout);
        await _dbContext.SaveChangesAsync();
    }
}
