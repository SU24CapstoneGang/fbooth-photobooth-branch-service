using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class ThemeStickerRepository : IThemeStickerRepository
    {
        private readonly AppDbContext _dbContext;

        public ThemeStickerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(ThemeSticker entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ThemeStickerID;
        }

        // Read
        public async Task<IQueryable<ThemeSticker>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.ThemeStickers.AsQueryable());
        }

        public async Task<IQueryable<ThemeSticker>> GetAsync(Expression<Func<ThemeSticker, bool>> predicate)
        {
            try
            {
                var result = _dbContext.ThemeStickers.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<ThemeSticker>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(ThemeSticker entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(ThemeSticker entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
