using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class EffectsPackLogRepository : IEffectsPackLogRepository
    {
        private readonly AppDbContext _dbContext;

        public EffectsPackLogRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(EffectsPackLog entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.PacklogID;
        }

        // Read
        public async Task<IQueryable<EffectsPackLog>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.EffectsPacks.AsQueryable());
        }

        public async Task<IQueryable<EffectsPackLog>> GetAsync(Expression<Func<EffectsPackLog, bool>> predicate)
        {
            try
            {
                var result = _dbContext.EffectsPacks.Where(predicate).AsQueryable();
                if (!result.Any())
                {
                    return await Task.FromResult(new List<EffectsPackLog>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(EffectsPackLog entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(EffectsPackLog entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
