using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class FilterRepository : IFilterRepository
{
    private readonly AppDbContext _dbContext;

    public FilterRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Guid> AddAsync(Filter filter)
    {
        await _dbContext.AddAsync(filter);
        await _dbContext.SaveChangesAsync();
        return filter.FilterID;
    }

    //Read
    public async Task<IQueryable<Filter>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Filters);
    }

    public async Task<IQueryable<Filter>> GetAsync(Expression<Func<Filter, bool>> predicate)
    {
        return await Task.FromResult(_dbContext.Filters.Where(predicate));
    }

    //Delete
    public async Task RemoveAsync(Filter filter)
    {
        _dbContext.Remove(filter);
        await _dbContext.SaveChangesAsync();
    }
    //Update
    public async Task UpdateAsync(Filter filter)
    {
        _dbContext.Update(filter);
        await _dbContext.SaveChangesAsync();
    }
}
