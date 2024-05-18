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

public class FilterRepository : IFilterRepository
{
    private readonly AppDbContext _dbContext;

    public FilterRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Filter filter)
    {
        await _dbContext.AddAsync(filter);
        await _dbContext.SaveChangesAsync();
        return filter.FilterID;
    }

    public async Task<IEnumerable<Filter>> GetAll()
    {
        return await _dbContext.Filters.ToListAsync();
    }

    public async Task<Filter?> GetByIdAsync(Guid filterId)
    {
        return await _dbContext.Filters.FindAsync(filterId);
    }

    public async Task<IEnumerable<Filter>> GetByName(string name)
    {
        return await _dbContext.Filters.Where(c => c.FilterName.Contains(name)).ToListAsync();
    }

    public async Task RemoveAsync(Filter filter)
    {
        _dbContext.Remove(filter);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Filter filter)
    {
        _dbContext.Update(filter);
        await _dbContext.SaveChangesAsync();
    }
}
