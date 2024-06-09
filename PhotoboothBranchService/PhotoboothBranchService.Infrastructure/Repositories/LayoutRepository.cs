using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
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

    public async Task<IQueryable<Layout>> GetAsync(Expression<Func<Layout, bool>> predicate)
    {
        try
        {
            var result = _dbContext.Layouts.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<Layout>().AsQueryable());
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
        _dbContext.Update(layout);
        await _dbContext.SaveChangesAsync();
    }
}
