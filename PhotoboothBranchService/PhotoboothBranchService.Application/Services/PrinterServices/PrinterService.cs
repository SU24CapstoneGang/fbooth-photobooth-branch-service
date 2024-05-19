using AutoMapper;
using Beanbox.Business.Commons.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Printer;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Printer;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.PrinterServices;

public class PrinterService : IPrinterService
{
    private readonly IPrinterRepository _printerRepository;
    private readonly IMapper _mapper;

    public PrinterService(IPrinterRepository printerRepository, IMapper mapper)
    {
        _printerRepository = printerRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(CreatePrinterRequest createModel)
    {
        Printer printer = _mapper.Map<Printer>(createModel);
        await _printerRepository.AddAsync(printer);
        return printer.PrinterID;
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var printer = (await _printerRepository.GetAsync(p => p.PrinterID == id)).FirstOrDefault();
            if (printer != null)
            {
                await _printerRepository.RemoveAsync(printer);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<PrinterResponse>> GetAllAsync()
    {
        var printers = await _printerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PrinterResponse>>(printers.ToList());
    }

    public async Task<IEnumerable<PrinterResponse>> GetAllPagingAsync(PrinterFilter filter, PagingModel paging)
    {
        var printers = (await _printerRepository.GetAllAsync()).AutoPaging(paging.PageSize,paging.PageIndex);
        var listPrinterresponse = _mapper.Map<IEnumerable<PrinterResponse>>(printers.ToList());
        listPrinterresponse.AutoFilter(filter);
        return listPrinterresponse;
    }

    public async Task<PrinterResponse> GetByIdAsync(Guid id)
    {
        var printer = (await _printerRepository.GetAsync(p => p.PrinterID == id)).FirstOrDefault();
        return _mapper.Map<PrinterResponse>(printer);
    }

    public async Task<IEnumerable<PrinterResponse>> GetByName(string name)
    {
        var printers = await _printerRepository.GetAsync(p => p.ModelName.Contains(name));
        return _mapper.Map<IEnumerable<PrinterResponse>>(printers.ToList());
    }

    public async Task UpdateAsync(Guid id, UpdatePrinterRequest updateModel)
    {
        var printer = (await _printerRepository.GetAsync(p => p.PrinterID == id)).FirstOrDefault();
        if (printer == null)
        {
            throw new KeyNotFoundException("Printer not found.");
        }

        var updatePrinter = _mapper.Map(updateModel, printer);
        await _printerRepository.UpdateAsync(updatePrinter);
    }
}

