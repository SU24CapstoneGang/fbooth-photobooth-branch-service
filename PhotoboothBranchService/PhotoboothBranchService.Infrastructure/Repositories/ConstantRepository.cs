using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Helper;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories
{
    public class ConstantRepository : IConstantRepository
    {
        private readonly AppDbContext _dbContext;

        public ConstantRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Create
        public async Task<Constant> AddAsync(Constant entity)
        {
            var result = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        //Read
        public async Task<IQueryable<Constant>> GetAllAsync()
        {
            return await Task.FromResult(_dbContext.Constants);
        }

        public async Task<IQueryable<Constant>> GetAsync(
            Expression<Func<Constant, bool>> predicate = null,
            params Expression<Func<Constant, object>>[] includeProperties)
        {
            try
            {
                var result = predicate == null ? _dbContext.Constants : _dbContext.Constants.Where(predicate);
                if (!result.Any())
                {
                    return await Task.FromResult(Enumerable.Empty<Constant>().AsQueryable());
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

        //Delete
        public async Task RemoveAsync(Constant entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        //Update
        public async Task UpdateAsync(Constant entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
