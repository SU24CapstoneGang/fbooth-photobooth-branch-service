using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.FrameServices;

public interface IFrameService : IService<FrameDTO>
{
    Task<IEnumerable<FrameDTO>> GetByName(string name);
}
