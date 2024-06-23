using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class PhotoStickerRepository : IPhotoStickerRepository
    {
        private readonly AppDbContext _dbContext;

        public PhotoStickerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<PhotoSticker> AddAsync(PhotoSticker entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<PhotoSticker>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.PhotoStickers.AsQueryable());
        }

        public async Task<IQueryable<PhotoSticker>> GetAsync(
        Expression<Func<PhotoSticker, bool>> predicate = null,
        params Expression<Func<PhotoSticker, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.PhotoStickers : _dbContext.PhotoStickers.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<PhotoSticker>().AsQueryable());
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
