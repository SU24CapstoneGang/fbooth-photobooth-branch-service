﻿using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

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
        public async Task<Service> AddAsync(Service entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        // Read
        public async Task<IQueryable<Service>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Services.AsQueryable());
        }

        public async Task<IQueryable<Service>> GetAsync(
        Expression<Func<Service, bool>> predicate = null,
        params Expression<Func<Service, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.Services : _dbContext.Services.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<Service>().AsQueryable());
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
