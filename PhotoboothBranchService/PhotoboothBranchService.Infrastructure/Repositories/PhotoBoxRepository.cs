using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class PhotoBoxRepository : IPhotoBoxRepository
    {
        private readonly AppDbContext _dbContext;

        public PhotoBoxRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PhotoBox> AddAsync(PhotoBox entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IQueryable<PhotoBox>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.PhotoBoxes.AsQueryable());
        }

        public async Task<IQueryable<PhotoBox>> GetAsync(Expression<Func<PhotoBox, bool>> predicate = null, params Expression<Func<PhotoBox, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.PhotoBoxes : _dbContext.PhotoBoxes.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<PhotoBox>().AsQueryable());
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

        public async Task RemoveAsync(PhotoBox entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PhotoBox entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
