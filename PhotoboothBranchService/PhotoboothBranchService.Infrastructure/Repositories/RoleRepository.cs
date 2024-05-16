using Microsoft.EntityFrameworkCore;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _dbContext;

    public RoleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Role Role)
    {
        await _dbContext.AddAsync(Role);
        await _dbContext.SaveChangesAsync();
        return Role.RoleID;
    }

    public async Task<IEnumerable<Role>> GetAll()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(Guid RoleID)
    {
        return await _dbContext.Roles.FindAsync(RoleID);
    }

    public async Task<IEnumerable<Role>> GetByName(string name)
    {
        return await _dbContext.Roles.Where(p => p.RoleName.Contains(name)).ToListAsync();
    }

    public async Task RemoveAsync(Role Role)
    {
        _dbContext.Roles.Remove(Role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Role Role)
    {
        _dbContext.Update(Role);
        await _dbContext.SaveChangesAsync();
    }
}
