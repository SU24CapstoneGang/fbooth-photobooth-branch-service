using CloudinaryDotNet;
using Google;
using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Common.Interfaces;
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
    public class FullPaymentPolicyRepository : IFullPaymentPolicyRepository
    {
        private readonly AppDbContext _dbContext;

        public FullPaymentPolicyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<FullPaymentPolicy>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.FullPaymentPolicies.AsQueryable());
        }

        public async Task<IQueryable<FullPaymentPolicy>> GetAsync(Expression<Func<FullPaymentPolicy, bool>> predicate = null, params Expression<Func<FullPaymentPolicy, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.FullPaymentPolicies : _dbContext.FullPaymentPolicies.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<FullPaymentPolicy>().AsQueryable());
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

        public async Task RemoveAsync(FullPaymentPolicy entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(FullPaymentPolicy entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FullPaymentPolicy> AddAsync(FullPaymentPolicy entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
