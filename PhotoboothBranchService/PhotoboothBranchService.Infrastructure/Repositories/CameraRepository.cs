using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;
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
    public async Task<Guid> AddAsync(Camera camera)
    {
        await _dbContext.AddAsync(camera);
        await _dbContext.SaveChangesAsync();
        return camera.CameraID;
    }

    //Read
    public async Task<IEnumerable<Camera>> GetAll()
    {
        return await _dbContext.Cameras.ToListAsync();
    }

    public async Task<Camera?> GetByIdAsync(Guid cameraId)
    {
        return await _dbContext.Cameras.FindAsync(cameraId);
    }

    public async Task<IEnumerable<Camera>> GetByName(string name)
    {
        return await _dbContext.Cameras.Where(c => c.ModelName.Contains(name)).ToListAsync();
    }

    //Update
    public async Task UpdateAsync(Camera camera)
    {
        _dbContext.Update(camera);
        await _dbContext.SaveChangesAsync();
    }

    //Delete
    public async Task RemoveAsync(Camera camera)
    {
        _dbContext.Remove(camera);
        await _dbContext.SaveChangesAsync();
    }

}
