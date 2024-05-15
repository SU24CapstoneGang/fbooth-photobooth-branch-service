using Microsoft.EntityFrameworkCore;
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

public class PhotoBoothBranchRepository : IPhotoBoothBranchRepository
{
    private readonly AppDbContext _dbContext;

    public PhotoBoothBranchRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Create
    public async Task<Guid> AddAsync(PhotoBoothBranch photoBoothBranch, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(photoBoothBranch, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return photoBoothBranch.BranchesID;
    }

    //Read
    public async Task<IEnumerable<PhotoBoothBranch>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PhotoBoothBranch>> GetAll(ManufactureStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.Where(p => p.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PhotoBoothBranch>> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.Where(p => p.BranchName.Contains(name)).ToListAsync(cancellationToken);
    }

    public async Task<PhotoBoothBranch?> GetByIdAsync(Guid photoBoothBranchId, CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.FindAsync(photoBoothBranchId, cancellationToken);
    }

    //Update
    public async Task UpdateAsync(PhotoBoothBranch photoBoothBranch, CancellationToken cancellationToken)
    {
        _dbContext.Update(photoBoothBranch);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    //Delete
    public async Task RemoveAsync(PhotoBoothBranch photoBoothBranch, CancellationToken cancellationToken)
    {
        _dbContext.Remove(photoBoothBranch);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

