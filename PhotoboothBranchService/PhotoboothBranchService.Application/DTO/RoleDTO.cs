using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class RoleDTO
{
    public Guid? RoleID { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}