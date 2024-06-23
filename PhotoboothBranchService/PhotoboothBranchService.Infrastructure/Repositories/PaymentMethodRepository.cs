using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly AppDbContext _dbContext;

        public PaymentMethodRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<PaymentMethod> AddAsync(PaymentMethod entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<PaymentMethod>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.PaymentMethods.AsQueryable());
        }

        public async Task<IQueryable<PaymentMethod>> GetAsync(
        Expression<Func<PaymentMethod, bool>> predicate = null,
        params Expression<Func<PaymentMethod, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.PaymentMethods : _dbContext.PaymentMethods.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<PaymentMethod>().AsQueryable());
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

        // Update
        public async Task UpdateAsync(PaymentMethod entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(PaymentMethod entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
