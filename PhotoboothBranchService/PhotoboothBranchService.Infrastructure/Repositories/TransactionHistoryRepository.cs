using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionHistoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(TransactionHistory entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.TransactionID;
        }

        // Read
        public async Task<IQueryable<TransactionHistory>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.TransactionHistories.AsQueryable());
        }

        public async Task<IQueryable<TransactionHistory>> GetAsync(Expression<Func<TransactionHistory, bool>> predicate)
        {
            try
            {
                var result = _dbContext.TransactionHistories.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<TransactionHistory>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(TransactionHistory entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(TransactionHistory entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
