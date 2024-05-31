using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Printer;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
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
        try
        {
            Printer printer = _mapper.Map<Printer>(createModel);
            printer.Status = ManufactureStatus.Active;
            await _printerRepository.AddAsync(printer);
            return printer.PrinterID;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while create Printer: " + ex.Message);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var printer = (await _printerRepository.GetAsync(p => p.PrinterID == id)).FirstOrDefault();
            if (printer == null) throw new NotFoundException("Printer", id, "Printer id not found");

            await _printerRepository.RemoveAsync(printer);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting Printer: " + ex.Message);
        }
    }

    public async Task<IEnumerable<PrinterResponse>> GetAllAsync()
    {
        try
        {
            var printers = await _printerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PrinterResponse>>(printers.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting Printer: " + ex.Message);
        }
    }

    public async Task<IEnumerable<PrinterResponse>> GetAllPagingAsync(PrinterFilter filter, PagingModel paging)
    {
        try
        {
            var printers = (await _printerRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listPrinterresponse = _mapper.Map<IEnumerable<PrinterResponse>>(printers);
            listPrinterresponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listPrinterresponse;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting Printer: " + ex.Message);
        }
    }

    public async Task<PrinterResponse> GetByIdAsync(Guid id)
    {
        try
        {
            var printer = (await _printerRepository.GetAsync(p => p.PrinterID == id)).FirstOrDefault();
            return _mapper.Map<PrinterResponse>(printer);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting Printer: " + ex.Message);
        }
    }

    public async Task<IEnumerable<PrinterResponse>> GetByName(string name)
    {
        try
        {
            var printers = await _printerRepository.GetAsync(p => p.ModelName.Contains(name));
            return _mapper.Map<IEnumerable<PrinterResponse>>(printers.ToList());
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting Printer: " + ex.Message);
        }
    }

    public async Task UpdateAsync(Guid id, UpdatePrinterRequest updateModel)
    {
        try
        {
            var printer = (await _printerRepository.GetAsync(p => p.PrinterID == id)).FirstOrDefault();
            if (printer == null) throw new NotFoundException("Printer", id, "Printer id not found");

            var updatePrinter = _mapper.Map(updateModel, printer);
            await _printerRepository.UpdateAsync(updatePrinter);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating Printer: " + ex.Message);
        }
    }
}

