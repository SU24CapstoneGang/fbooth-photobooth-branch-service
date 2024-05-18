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

public class LayoutRepository : ILayoutRepository
{
    private readonly AppDbContext _dbContext;

    public LayoutRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Layout layout)
    {
        await _dbContext.AddAsync(layout);
        await _dbContext.SaveChangesAsync();
        return layout.LayoutID;
    }

    public async Task<IEnumerable<Layout>> GetAll()
    {
        return await _dbContext.Layouts.ToListAsync();
    }

    public async Task<Layout?> GetByIdAsync(Guid layoutId)
    {
        return await _dbContext.Layouts.FindAsync(layoutId);
    }

    public async Task RemoveAsync(Layout layout)
    {
        _dbContext.Remove(layout);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Layout layout)
    {
        _dbContext.Update(layout);
        await _dbContext.SaveChangesAsync();
    }
}
