using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAll();
    Task<IEnumerable<Role>> GetByName(string name);
    Task<Guid> AddAsync(Role Role);
    Task<Role?> GetByIdAsync(Guid RoleID);
    Task RemoveAsync(Role Role);
    Task UpdateAsync(Role Role);
}
