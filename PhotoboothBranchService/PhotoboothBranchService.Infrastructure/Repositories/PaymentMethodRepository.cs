using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public async Task<Guid> AddAsync(PaymentMethod entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.PaymentMethodID;
        }

        // Read
        public async Task<IQueryable<PaymentMethod>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.PaymentMethods.AsQueryable());
        }

        public async Task<IQueryable<PaymentMethod>> GetAsync(Expression<Func<PaymentMethod, bool>> predicate)
        {
            return await Task.FromResult(_dbContext.PaymentMethods.Where(predicate).AsQueryable());
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
