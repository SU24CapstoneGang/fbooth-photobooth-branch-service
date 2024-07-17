using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        private readonly AppDbContext _dbContext;

        public RefundRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Refund> AddAsync(Refund entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<Refund>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Refunds.AsQueryable());
        }

        public async Task<IQueryable<Refund>> GetAsync(
            Expression<Func<Refund, bool>> predicate = null,
            params Expression<Func<Refund, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.Refunds : _dbContext.Refunds.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<Refund>().AsQueryable());
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
        public async Task UpdateAsync(Refund entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(Refund entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
