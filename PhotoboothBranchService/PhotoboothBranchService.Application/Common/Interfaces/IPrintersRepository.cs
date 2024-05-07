using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Common.Interfaces;

public interface IPrintersRepository
{
    Task<IEnumerable<Printers>> GetAll(CancellationToken cancellationToken);
    Task<IEnumerable<Printers>> GetByName(String name, CancellationToken cancellationToken);
    Task AddAsync(Printers printer, CancellationToken cancellationToken);
    Task<Printers?> GetByIdAsync(Guid printerId, CancellationToken cancellationToken);
    Task RemoveAsync(Printers printer, CancellationToken cancellationToken);
    Task UpdateAsync(Printers printera, CancellationToken cancellationToken);
}
