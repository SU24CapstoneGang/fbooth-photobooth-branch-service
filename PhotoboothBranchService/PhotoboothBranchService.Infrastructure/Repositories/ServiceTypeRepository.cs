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
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public ServiceTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(ServiceType entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ServiceTypeId;
        }

        // Read
        public async Task<IQueryable<ServiceType>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.ServiceTypes.AsQueryable());
        }

        public async Task<IQueryable<ServiceType>> GetAsync(Expression<Func<ServiceType, bool>> predicate)
        {
            try
            {
                var result = _dbContext.ServiceTypes.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<ServiceType>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(ServiceType entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(ServiceType entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
