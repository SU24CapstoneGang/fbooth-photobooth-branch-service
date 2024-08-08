using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class BranchPhotoRepository : IBranchPhotoRepository
    {
        private readonly AppDbContext _dbContext;

        public BranchPhotoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BranchPhoto> AddAsync(BranchPhoto entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IQueryable<BranchPhoto>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.BranchPhotos);
        }

        public async Task<IQueryable<BranchPhoto>> GetAsync(Expression<Func<BranchPhoto, bool>> predicate = null, params Expression<Func<BranchPhoto, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.BranchPhotos : _dbContext.BranchPhotos.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<BranchPhoto>().AsQueryable());
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

        public async Task RemoveAsync(BranchPhoto entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BranchPhoto entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
