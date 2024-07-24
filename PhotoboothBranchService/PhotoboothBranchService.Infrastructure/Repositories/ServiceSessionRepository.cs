using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class ServiceSessionRepository : IServiceSessionRepository
    {
        private readonly AppDbContext _dbContext;

        public ServiceSessionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<ServiceSession> AddAsync(ServiceSession entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<ServiceSession>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.ServiceSessions.AsQueryable());
        }

        public async Task<IQueryable<ServiceSession>> GetAsync(
        Expression<Func<ServiceSession, bool>> predicate = null,
        params Expression<Func<ServiceSession, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.ServiceSessions : _dbContext.ServiceSessions.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<ServiceSession>().AsQueryable());
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
        public async Task UpdateAsync(ServiceSession entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(ServiceSession entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
