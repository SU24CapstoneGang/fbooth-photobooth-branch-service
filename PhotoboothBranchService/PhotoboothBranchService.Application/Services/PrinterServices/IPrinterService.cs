using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Printer;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PrinterServices;

public interface IPrinterService : IService<PrinterResponse, CreatePrinterRequest, UpdatePrinterRequest, PrinterFilter, PagingModel>
{
    Task<IEnumerable<PrinterResponse>> GetByName(string name);
}
