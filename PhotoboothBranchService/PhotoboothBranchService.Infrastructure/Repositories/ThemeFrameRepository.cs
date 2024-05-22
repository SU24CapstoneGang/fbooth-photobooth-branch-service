using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class ThemeFrameRepository : IThemeFrameRepository
    {
        private readonly AppDbContext _dbContext;

        public ThemeFrameRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(ThemeFrame entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ThemeFrameID;
        }

        // Read
        public async Task<IQueryable<ThemeFrame>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.ThemeFrames.AsQueryable());
        }

        public async Task<IQueryable<ThemeFrame>> GetAsync(Expression<Func<ThemeFrame, bool>> predicate)
        {
            return await Task.FromResult(_dbContext.ThemeFrames.Where(predicate).AsQueryable());
        }

        // Update
        public async Task UpdateAsync(ThemeFrame entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(ThemeFrame entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
