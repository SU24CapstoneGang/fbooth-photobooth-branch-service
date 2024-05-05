using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class CamerasRepository : ICamerasRepository
{
    private readonly AppDbContext _dbContext;
    public CamerasRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Cameras camera, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(camera, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Cameras>> GetAll(ManufactureStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.Cameras.Where(c=> c.Status == status).ToListAsync();
    }

    public async Task<IEnumerable<Cameras>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Cameras.ToListAsync();
    }

    public async Task<Cameras?> GetByIdAsync(Guid cameraId, CancellationToken cancellationToken)
    {
        return await _dbContext.Cameras.FindAsync(cameraId, cancellationToken);
    }

    public async Task<IEnumerable<Cameras>> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Cameras.Where(c => c.ModelName.Contains(name)).ToListAsync();
    }

    public async Task RemoveAsync(Cameras camera, CancellationToken cancellationToken)
    {
        _dbContext.Remove(camera);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Cameras camera, CancellationToken cancellationToken)
    {
        _dbContext.Update(camera);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
