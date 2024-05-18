using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.IRepository;

public interface ILayoutRepository
{
    Task<IEnumerable<Layout>> GetAll();
    Task<Guid> AddAsync(Layout layout);
    Task<Layout?> GetByIdAsync(Guid layoutId);
    Task RemoveAsync(Layout layout);
    Task UpdateAsync(Layout layout);
}
