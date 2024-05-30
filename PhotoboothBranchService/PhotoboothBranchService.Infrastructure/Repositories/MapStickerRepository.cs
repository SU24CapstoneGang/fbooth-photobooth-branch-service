using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public async Task<Guid> AddAsync(MapSticker entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.MapStickerID;
        }

        // Read
        public async Task<IQueryable<MapSticker>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.MapStickers.AsQueryable());
        }

        public async Task<IQueryable<MapSticker>> GetAsync(Expression<Func<MapSticker, bool>> predicate)
        {
            try
            {
                var result = _dbContext.MapStickers.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<MapSticker>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(MapSticker entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(MapSticker entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
