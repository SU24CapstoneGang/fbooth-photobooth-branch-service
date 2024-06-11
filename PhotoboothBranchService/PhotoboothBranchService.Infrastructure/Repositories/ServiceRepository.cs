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
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _dbContext;

        public ServiceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(Service entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ServiceId;
        }

        // Read
        public async Task<IQueryable<Service>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Services.AsQueryable());
        }

        public async Task<IQueryable<Service>> GetAsync(Expression<Func<Service, bool>> predicate)
        {
            try
            {
                var result = _dbContext.Services.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<Service>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(Service entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(Service entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
