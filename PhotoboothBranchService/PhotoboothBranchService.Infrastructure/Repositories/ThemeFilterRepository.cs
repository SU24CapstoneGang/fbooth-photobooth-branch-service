using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class ThemeFilterRepository : IThemeFilterRepository
    {
        private readonly AppDbContext _dbContext;

        public ThemeFilterRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(ThemeFilter entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ThemeFilterID;
        }

        // Read
        public async Task<IQueryable<ThemeFilter>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.ThemeFilters.AsQueryable());
        }

        public async Task<IQueryable<ThemeFilter>> GetAsync(Expression<Func<ThemeFilter, bool>> predicate)
        {
            try
            {
                var result = _dbContext.ThemeFilters.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<ThemeFilter>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(ThemeFilter entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(ThemeFilter entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
