using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Common.Interfaces;

public interface IDeviceMapRepository
{
    Task<IEnumerable<PhotoBoothBranches>> GetAll(CancellationToken cancellationToken);
    Task AddAsync(DeviceMap deviceMap, CancellationToken cancellationToken);
    Task<DeviceMap?> GetByIdAsync(Guid deviceMapId, CancellationToken cancellationToken);
    Task RemoveAsync(DeviceMap deviceMap, CancellationToken cancellationToken);
    Task UpdateAsync(DeviceMap deviceMap, CancellationToken cancellationToken);
}
