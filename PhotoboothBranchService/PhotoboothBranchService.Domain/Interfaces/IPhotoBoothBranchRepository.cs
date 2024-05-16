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
    Task<IEnumerable<PhotoBoothBranch>> GetAll();
    Task<IEnumerable<PhotoBoothBranch>> GetAll(ManufactureStatus status);
    Task<IEnumerable<PhotoBoothBranch>> GetByName(string name);
    Task<Guid> AddAsync(PhotoBoothBranch photoBoothBranch);
    Task<PhotoBoothBranch?> GetByIdAsync(Guid photoBoothBranchId);
    Task RemoveAsync(PhotoBoothBranch photoBoothBranch);
    Task UpdateAsync(PhotoBoothBranch photoBoothBranch);
}
