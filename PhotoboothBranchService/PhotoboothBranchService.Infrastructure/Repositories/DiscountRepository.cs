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

public class DiscountRepository : IDiscountRepository
{
    private readonly AppDbContext _dbContext;

    public DiscountRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Add a new Discount to the database
    public async Task<Guid> AddAsync(Discount discount)
    {
        discount.DiscountID = Guid.NewGuid();
        await _dbContext.Discounts.AddAsync(discount);
        await _dbContext.SaveChangesAsync();
        return discount.DiscountID;
    }

    // Get all Discounts from the database
    public async Task<IEnumerable<Discount>> GetAll()
    {
        return await _dbContext.Discounts.ToListAsync();
    }

    public async Task<IEnumerable<Discount>> GetByCode(string code)
    {
        return await _dbContext.Discounts.Where(d => d.DiscountCode == code).ToListAsync();
    }

    public async Task<Discount?> GetByIdAsync(Guid discountId)
    {
        return await _dbContext.Discounts.FindAsync(discountId);
    }

    public async Task RemoveAsync(Discount discount)
    {
        _dbContext.Discounts.Remove(discount);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Discount discount)
    {
        _dbContext.Discounts.Update(discount);
        await _dbContext.SaveChangesAsync();
    }
}
