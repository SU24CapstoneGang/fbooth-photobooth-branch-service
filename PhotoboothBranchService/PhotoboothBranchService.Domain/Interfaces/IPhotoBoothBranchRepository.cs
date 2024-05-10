using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Interfaces;

public interface IPhotoBoothBranchRepository
{
    Task<IEnumerable<PhotoBoothBranches>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<PhotoBoothBranches>> GetAll(ManufactureStatus status, CancellationToken cancellationToken);
    Task<IEnumerable<PhotoBoothBranches>> GetByName(string name, CancellationToken cancellationToken);
    Task<Guid> AddAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken);
    Task<PhotoBoothBranches?> GetByIdAsync(Guid photoBoothBranchId, CancellationToken cancellationToken);
    Task RemoveAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken);
    Task UpdateAsync(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken);
}
