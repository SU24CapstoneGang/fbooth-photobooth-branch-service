using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class BoothPhotoRepository : IBoothPhotoRepository
    {
        private readonly AppDbContext _dbContext;

        public BoothPhotoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BoothPhoto> AddAsync(BoothPhoto entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IQueryable<BoothPhoto>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.BoothPhotos);
        }

        public async Task<IQueryable<BoothPhoto>> GetAsync(Expression<Func<BoothPhoto, bool>> predicate = null, params Expression<Func<BoothPhoto, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.BoothPhotos : _dbContext.BoothPhotos.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<BoothPhoto>().AsQueryable());
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

        public async Task RemoveAsync(BoothPhoto entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BoothPhoto entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
