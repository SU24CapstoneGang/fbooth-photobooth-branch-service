﻿using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

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
        public async Task<Photo> AddAsync(Photo entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<Photo>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Photos.AsQueryable());
        }

        public async Task<IQueryable<Photo>> GetAsync(
        Expression<Func<Photo, bool>> predicate = null,
        params Expression<Func<Photo, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.Photos : _dbContext.Photos.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<Photo>().AsQueryable());
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
