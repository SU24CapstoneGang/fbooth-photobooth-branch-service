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
    public class BoothRepository : IBoothRepository
    {
        private readonly AppDbContext _dbContext;

        public BoothRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        public async Task<Guid> AddAsync(Booth entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.BoothID;
        }

        // Read
        public async Task<IQueryable<Booth>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Booths);
        }

        public async Task<IQueryable<Booth>> GetAsync(Expression<Func<Booth, bool>> predicate)
        {
            try
            {
                var result = _dbContext.Booths.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(new List<Booth>().AsQueryable());
                }
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Update
        public async Task UpdateAsync(Booth entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        // Delete
        public async Task RemoveAsync(Booth entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
