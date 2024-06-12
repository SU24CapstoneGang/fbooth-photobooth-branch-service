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
    public class ServiceItemRepository : IServiceItemRepository
    {
        private readonly AppDbContext _dbContext;

        public ServiceItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(ServiceItem entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ServiceItemID;
        }

        // Read
        public async Task<IQueryable<ServiceItem>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.ServiceItems.AsQueryable());
        }

        public async Task<IQueryable<ServiceItem>> GetAsync(Expression<Func<ServiceItem, bool>> predicate)
        {
            try
            {
                var result = _dbContext.ServiceItems.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<ServiceItem>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(ServiceItem entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(ServiceItem entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
