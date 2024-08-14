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
    public class StickerTypeRepository : IStickerTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public StickerTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<StickerType> AddAsync(StickerType entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<StickerType>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.StickerTypes);
        }

        public async Task<IQueryable<StickerType>> GetAsync(
            Expression<Func<StickerType, bool>> predicate = null,
            params Expression<Func<StickerType, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.StickerTypes : _dbContext.StickerTypes.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<StickerType>().AsQueryable());
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

        // Delete
        public async Task RemoveAsync(StickerType entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Update
        public async Task UpdateAsync(StickerType entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
