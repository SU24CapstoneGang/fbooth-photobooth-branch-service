using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IService<TEntityDTO>
{
    Task<TEntityDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntityDTO>> GetAllAsync();
    Task<Guid> CreateAsync(TEntityDTO entityDTO);
    Task UpdateAsync(Guid id, TEntityDTO entityDTO);
    Task DeleteAsync(Guid id);
}