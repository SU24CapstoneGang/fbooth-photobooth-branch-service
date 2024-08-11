using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class SlotRepository : ISlotRepository
    {
        private readonly AppDbContext _dbContext;

        public SlotRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Slot> AddAsync(Slot entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<Slot>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Slots.AsQueryable());
        }

        public async Task<IQueryable<Slot>> GetAsync(
            Expression<Func<Slot, bool>> predicate = null,
            params Expression<Func<Slot, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.Slots : _dbContext.Slots.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<Slot>().AsQueryable());
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
        public async Task UpdateAsync(Slot entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(Slot entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
