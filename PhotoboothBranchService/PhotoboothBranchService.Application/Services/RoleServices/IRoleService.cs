using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.RoleServices;

public interface IRoleService : IService<RoleDTO>
{
    Task<IEnumerable<RoleDTO>> GetByName(string name);
}
