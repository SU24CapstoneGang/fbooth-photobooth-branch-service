using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class CameraRepository : ICameraRepository
{
    private readonly AppDbContext _dbContext;
    public CameraRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Guid> AddAsync(Camera camera, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(camera, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return camera.CameraID;
    }

    //Read
    public async Task<IEnumerable<Camera>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.Cameras.ToListAsync();
    }

    public async Task<Camera?> GetByIdAsync(Guid cameraId, CancellationToken cancellationToken)
    {
        return await _dbContext.Cameras.FindAsync(cameraId, cancellationToken);
    }

    public async Task<IEnumerable<Camera>> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.Cameras.Where(c => c.ModelName.Contains(name)).ToListAsync();
    }

    //Update
    public async Task UpdateAsync(Camera camera, CancellationToken cancellationToken)
    {
        _dbContext.Update(camera);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    //Delete
    public async Task RemoveAsync(Camera camera, CancellationToken cancellationToken)
    {
        _dbContext.Remove(camera);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

}
