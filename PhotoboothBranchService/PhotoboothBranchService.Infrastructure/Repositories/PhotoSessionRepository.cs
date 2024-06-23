using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class PhotoSessionRepository : IPhotoSessionRepository
    {
        private readonly AppDbContext _dbContext;

        public PhotoSessionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<PhotoSession> AddAsync(PhotoSession entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<PhotoSession>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.PhotoSessions.AsQueryable());
        }

        public async Task<IQueryable<PhotoSession>> GetAsync(
        Expression<Func<PhotoSession, bool>> predicate = null,
        params Expression<Func<PhotoSession, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.PhotoSessions : _dbContext.PhotoSessions.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<PhotoSession>().AsQueryable());
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
        public async Task UpdateAsync(PhotoSession entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(PhotoSession entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
