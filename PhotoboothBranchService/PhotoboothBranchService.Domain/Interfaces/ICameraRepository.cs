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
    Task<IEnumerable<Camera>> GetAll();
    Task<IEnumerable<Camera>> GetByName(string name);
    Task<Guid> AddAsync(Camera camera);
    Task<Camera?> GetByIdAsync(Guid cameraId);
    Task RemoveAsync(Camera camera);
    Task UpdateAsync(Camera camera);
}
