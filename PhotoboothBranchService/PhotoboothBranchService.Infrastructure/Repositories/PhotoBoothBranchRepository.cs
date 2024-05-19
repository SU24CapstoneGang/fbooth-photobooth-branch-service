using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using PhotoboothBranchService.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace PhotoboothBranchService.Infrastructure.Repositories;

public class PhotoBoothBranchRepository : IPhotoBoothBranchRepository
{
    private readonly AppDbContext _dbContext;

    public PhotoBoothBranchRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Guid> AddAsync(PhotoBoothBranch photoBoothBranch)
    {
        await _dbContext.AddAsync(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
        return photoBoothBranch.PhotoBoothBranchID;
    }

    //Read
    public async Task<IQueryable<PhotoBoothBranch>> GetAllAsync()
    {
        return await Task.FromResult(_dbContext.PhotoBoothBranches);
    }

    public async Task<IQueryable<PhotoBoothBranch>> GetAsync(Expression<Func<PhotoBoothBranch, bool>> predicate)
    {
        return await Task.FromResult(_dbContext.PhotoBoothBranches.Where(predicate));
    }

    //Update
    public async Task UpdateAsync(PhotoBoothBranch photoBoothBranch)
    {
        _dbContext.Update(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
    }

    //Delete
    public async Task RemoveAsync(PhotoBoothBranch photoBoothBranch)
    {
        _dbContext.Remove(photoBoothBranch);
        await _dbContext.SaveChangesAsync();
    }
}

