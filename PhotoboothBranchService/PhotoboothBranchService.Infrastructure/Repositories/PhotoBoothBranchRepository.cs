using Microsoft.EntityFrameworkCore;
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
        return photoBoothBranch.BranchesID;
    }

    //Read
    public async Task<IEnumerable<PhotoBoothBranch>> GetAll( )
    {
        return await _dbContext.PhotoBoothBranches.ToListAsync();
    }

    public async Task<IEnumerable<PhotoBoothBranch>> GetAll(ManufactureStatus status)
    {
        return await _dbContext.PhotoBoothBranches.Where(p => p.Status == status).ToListAsync();
    }

    public async Task<IEnumerable<PhotoBoothBranch>> GetByName(string name)
    {
        return await _dbContext.PhotoBoothBranches.Where(p => p.BranchName.Contains(name)).ToListAsync();
    }

    public async Task<PhotoBoothBranch?> GetByIdAsync(Guid photoBoothBranchId)
    {
        return await _dbContext.PhotoBoothBranches.FindAsync(photoBoothBranchId);
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

