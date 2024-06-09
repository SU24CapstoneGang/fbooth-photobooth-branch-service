using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

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
        public async Task<Guid> AddAsync(Theme entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ThemeID;
        }

        // Read
        public async Task<IQueryable<Theme>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Themes.AsQueryable());
        }

        public async Task<IQueryable<Theme>> GetAsync(Expression<Func<Theme, bool>> predicate)
        {
            try
            {
                var result = _dbContext.Themes.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<Theme>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(Theme entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(Theme entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
