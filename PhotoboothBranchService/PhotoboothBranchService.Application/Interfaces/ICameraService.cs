using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Interfaces;

public interface ICameraService : IService<CameraDTO>
{
    Task<IEnumerable<CameraDTO>> GetByName(string name, CancellationToken cancellationToken);
}
