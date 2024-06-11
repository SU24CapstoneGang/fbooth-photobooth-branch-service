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
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AppDbContext _dbContext;

        public PhotoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(Photo entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.PhotoID;
        }

        // Read
        public async Task<IQueryable<Photo>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Photos.AsQueryable());
        }

        public async Task<IQueryable<Photo>> GetAsync(Expression<Func<Photo, bool>> predicate)
        {
            try
            {
                var result = _dbContext.Photos.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<Photo>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(Photo entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(Photo entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
