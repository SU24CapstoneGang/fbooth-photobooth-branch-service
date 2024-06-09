using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class MapStickerRepository : IMapStickerRepository
    {
        private readonly AppDbContext _dbContext;

        public MapStickerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(PhotoSticker entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.PhotoStickerID;
        }

        // Read
        public async Task<IQueryable<PhotoSticker>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.MapStickers.AsQueryable());
        }

        public async Task<IQueryable<PhotoSticker>> GetAsync(Expression<Func<PhotoSticker, bool>> predicate)
        {
            try
            {
                var result = _dbContext.MapStickers.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<PhotoSticker>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(PhotoSticker entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(PhotoSticker entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
