using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq;
using System.Linq.Expressions;

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
        await _dbContext.Discounts.AddAsync(discount);
        await _dbContext.SaveChangesAsync();
        return discount.DiscountID;
    }

    // Get all Discounts from the database
    public async Task<IQueryable<Discount>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Discounts);
    }

    public async Task<IQueryable<Discount>> GetAsync(Expression<Func<Discount, bool>> predicate)
    {
        try
        {
            var result = _dbContext.Discounts.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<Discount>().AsQueryable());
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<Discount>> GetByCode(string code)
    {
        return await _dbContext.Discounts.Where(d => d.DiscountCode == code).ToListAsync();
    }

    public async Task<Discount?> GetByIdAsync(Guid discountId)
    {
        return await _dbContext.Discounts.FindAsync(discountId);
    }

    //Delete
    public async Task RemoveAsync(Discount discount)
    {
        _dbContext.Discounts.Remove(discount);
        await _dbContext.SaveChangesAsync();
    }

    //Update
    public async Task UpdateAsync(Discount discount)
    {
        _dbContext.Discounts.Update(discount);
        await _dbContext.SaveChangesAsync();
    }
}
