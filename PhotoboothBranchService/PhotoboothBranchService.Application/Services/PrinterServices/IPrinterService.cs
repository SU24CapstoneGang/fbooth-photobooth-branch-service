using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PrinterServices;

public interface IPrinterService : IService<PrinterDTO>
{
    Task<IEnumerable<PrinterDTO>> GetByName(string name);
}
