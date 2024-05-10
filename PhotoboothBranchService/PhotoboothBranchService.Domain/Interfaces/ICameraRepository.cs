using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Interfaces;

public interface ICameraRepository
{
    Task<IEnumerable<Cameras>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<Cameras>> GetByName(string name, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Cameras camera, CancellationToken cancellationToken);
    Task<Cameras?> GetByIdAsync(Guid cameraId, CancellationToken cancellationToken);
    Task RemoveAsync(Cameras camera, CancellationToken cancellationToken);
    Task UpdateAsync(Cameras camera, CancellationToken cancellationToken);
}
