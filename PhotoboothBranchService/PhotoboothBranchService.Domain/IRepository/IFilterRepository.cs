using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Interfaces;

public interface IFilterRepository
{
    Task<IEnumerable<Filter>> GetAll();
    Task<IEnumerable<Filter>> GetByName(string name);
    Task<Guid> AddAsync(Filter filter);
    Task<Filter?> GetByIdAsync(Guid filterId);
    Task RemoveAsync(Filter filter);
    Task UpdateAsync(Filter filter);
}
