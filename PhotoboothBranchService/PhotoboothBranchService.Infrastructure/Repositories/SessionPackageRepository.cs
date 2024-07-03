using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class SessionPackageRepository : ISessionPackageRepository
    {
        private readonly AppDbContext _dbContext;

        public SessionPackageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<SessionPackage> AddAsync(SessionPackage entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<SessionPackage>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.SessionPackages);
        }

        public async Task<IQueryable<SessionPackage>> GetAsync(
            Expression<Func<SessionPackage, bool>> predicate = null,
            params Expression<Func<SessionPackage, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.SessionPackages : _dbContext.SessionPackages.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<SessionPackage>().AsQueryable());
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
        public async Task RemoveAsync(SessionPackage entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Update
        public async Task UpdateAsync(SessionPackage entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
