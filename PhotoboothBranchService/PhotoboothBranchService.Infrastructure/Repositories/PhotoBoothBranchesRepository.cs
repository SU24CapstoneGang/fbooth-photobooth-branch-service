using Microsoft.EntityFrameworkCore;
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

public class PhotoBoothBranchesRepository : IPhotoBoothBranchesRepository
{
    private readonly AppDbContext _dbContext;

    public PhotoBoothBranchesRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PhotoBoothBranches>> GetAll(CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PhotoBoothBranches>> GetAll(ManufactureStatus status, CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.Where(p => p.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PhotoBoothBranches>> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.Where(p => p.BranchName.Contains(name)).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(photoBoothBranch, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PhotoBoothBranches?> GetByIdAsync(Guid photoBoothBranchId, CancellationToken cancellationToken)
    {
        return await _dbContext.PhotoBoothBranches.FindAsync(photoBoothBranchId, cancellationToken);
    }

    public async Task RemoveAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken)
    {
        _dbContext.Remove(photoBoothBranch);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken)
    {
        _dbContext.Update(photoBoothBranch);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

