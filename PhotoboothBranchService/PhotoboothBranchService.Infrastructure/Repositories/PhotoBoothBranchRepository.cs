using Microsoft.EntityFrameworkCore;
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
        return await Task.FromResult(_dbContext.PhotoBoothBranches.Include(b => b.Camera)
        .Include(b => b.Printer)
        .Include(b => b.Sessions));
    }

    public async Task<IQueryable<PhotoBoothBranch>> GetAsync(Expression<Func<PhotoBoothBranch, bool>> predicate)
    {
        try
        {
            var result = _dbContext.PhotoBoothBranches.Where(predicate);
            if (!result.Any())
            {
                return await Task.FromResult(new List<PhotoBoothBranch>().AsQueryable());
            }
            return await Task.FromResult(result);
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
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

