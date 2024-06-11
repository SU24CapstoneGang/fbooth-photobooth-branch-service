using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public async Task<Guid> AddAsync(PhotoSession entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.PhotoSessionID;
        }

        // Read
        public async Task<IQueryable<PhotoSession>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.PhotoSessions.AsQueryable());
        }

        public async Task<IQueryable<PhotoSession>> GetAsync(Expression<Func<PhotoSession, bool>> predicate)
        {
            try
            {
                var result = _dbContext.PhotoSessions.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<PhotoSession>().AsQueryable());
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
