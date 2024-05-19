using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

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
    public async Task<IQueryable<Camera>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.Cameras.AsQueryable());
    }
    public async Task<IQueryable<Camera>> GetAsync(Expression<Func<Camera, bool>> predicate)
    {
        return await Task.FromResult(_dbContext.Cameras.Where(predicate).AsQueryable());
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
