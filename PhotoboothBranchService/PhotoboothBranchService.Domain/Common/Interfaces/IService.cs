using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Common.Interfaces;

public interface IService<TEntityDTO>
{
    Task<TEntityDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntityDTO>> GetAllAsync(CancellationToken cancellationToken);
    Task<Guid> CreateAsync(TEntityDTO entityDTO, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, TEntityDTO entityDTO, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}