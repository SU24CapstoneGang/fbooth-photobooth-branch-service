using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(Order entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.OrderID;
        }

        // Read
        public async Task<IQueryable<Order>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Orders.AsQueryable());
        }

        public async Task<IQueryable<Order>> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            return await Task.FromResult(_dbContext.Orders.Where(predicate).AsQueryable());
        }

        // Update
        public async Task UpdateAsync(Order entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(Order entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
