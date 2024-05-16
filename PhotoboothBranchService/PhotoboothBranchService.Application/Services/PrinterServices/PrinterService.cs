using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<Guid> CreateAsync(PrinterDTO entityDTO)
    {
        Printer printer = _mapper.Map<Printer>(entityDTO);
        printer.PrinterID = Guid.NewGuid();
        await _printerRepository.AddAsync(printer);
        return printer.PrinterID;
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var printer = await _printerRepository.GetByIdAsync(id);
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

    public async Task<IEnumerable<PrinterDTO>> GetAllAsync()
    {
        var printers = await _printerRepository.GetAll();
        return _mapper.Map<IEnumerable<PrinterDTO>>(printers);
    }

    public async Task<PrinterDTO> GetByIdAsync(Guid id)
    {
        var printer = await _printerRepository.GetByIdAsync(id);
        return _mapper.Map<PrinterDTO>(printer);
    }

    public async Task<IEnumerable<PrinterDTO>> GetByName(string name)
    {
        var printers = await _printerRepository.GetByName(name);
        return _mapper.Map<IEnumerable<PrinterDTO>>(printers);
    }

    public async Task UpdateAsync(Guid id, PrinterDTO entityDTO)
    {
        entityDTO.PrinterId = id;
        Printer printer = _mapper.Map<Printer>(entityDTO);
        await _printerRepository.UpdateAsync(printer);
    }
}

