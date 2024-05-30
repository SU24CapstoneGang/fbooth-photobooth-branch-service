using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _dbContext;

    public RoleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    //Create
    public async Task<Guid> AddAsync(Role Role)
    {
        await _dbContext.AddAsync(Role);
        await _dbContext.SaveChangesAsync();
        return Role.RoleID;
    }

    //Read
    public async Task<IQueryable<Role>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Roles);
    }

    public async Task<IQueryable<Role>> GetAsync(Expression<Func<Role, bool>> predicate)
    {
        try
        {
            var result = _dbContext.Roles.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<Role>().AsQueryable());
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }

    //Delete
    public async Task RemoveAsync(Role Role)
    {
        _dbContext.Roles.Remove(Role);
        await _dbContext.SaveChangesAsync();
    }

    //Update
    public async Task UpdateAsync(Role Role)
    {
        _dbContext.Update(Role);
        await _dbContext.SaveChangesAsync();
    }
}
