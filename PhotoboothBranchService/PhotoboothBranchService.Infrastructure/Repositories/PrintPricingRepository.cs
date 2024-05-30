using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class PrintPricingRepository : IPrintPricingRepository
    {
        private readonly AppDbContext _dbContext;

        public PrintPricingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(PrintPricing entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.PrintPricingID;
        }

        // Read
        public async Task<IQueryable<PrintPricing>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.PrintPricings.AsQueryable());
        }

        public async Task<IQueryable<PrintPricing>> GetAsync(Expression<Func<PrintPricing, bool>> predicate)
        {
            try
            {
                var result = _dbContext.PrintPricings.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<PrintPricing>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(PrintPricing entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(PrintPricing entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
