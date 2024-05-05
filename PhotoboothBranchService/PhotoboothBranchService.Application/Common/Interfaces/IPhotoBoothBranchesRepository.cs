using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Common.Interfaces;

public interface IPhotoBoothBranchesRepository
{
    Task<IEnumerable<PhotoBoothBranches>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<PhotoBoothBranches>> GetAll(ManufactureStatus status, CancellationToken cancellationToken);
    Task<IEnumerable<PhotoBoothBranches>> GetByName(String name, CancellationToken cancellationToken);
    Task AddAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken);
    Task<PhotoBoothBranches?> GetByIdAsync(Guid photoBoothBranchId, CancellationToken cancellationToken);
    Task RemoveAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken);
    Task UpdateAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken);
}
