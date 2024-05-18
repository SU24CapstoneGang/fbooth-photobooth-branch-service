using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.IRepository;

public interface IPrinterRepository
{
    Task<IEnumerable<Printer>> GetAll();
    Task<IEnumerable<Printer>> GetByName(string name);
    Task<Guid> AddAsync(Printer printer);
    Task<Printer?> GetByIdAsync(Guid printerId);
    Task RemoveAsync(Printer printer);
    Task UpdateAsync(Printer printera);
}
