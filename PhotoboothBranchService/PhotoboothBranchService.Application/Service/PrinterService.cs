using AutoMapper;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Service;

public class PrinterService : IPrinterService
{
    private readonly IPrinterRepository _printerRepository;
    private readonly IMapper _mapper;

    public PrinterService(IPrinterRepository printerRepository, IMapper mapper)
    {
        _printerRepository = printerRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(PrinterDTO entityDTO, CancellationToken cancellationToken)
    {
        Printers printer = _mapper.Map<Printers>(entityDTO);
        printer.Id = Guid.NewGuid();
        await _printerRepository.AddAsync(printer, cancellationToken);
        return printer.Id;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var printer = await _printerRepository.GetByIdAsync(id, cancellationToken);
            if (printer != null)
            {
                await _printerRepository.RemoveAsync(printer, cancellationToken);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<PrinterDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var printers = await _printerRepository.GetAll(cancellationToken);
        return _mapper.Map<IEnumerable<PrinterDTO>>(printers);
    }

    public async Task<PrinterDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var printer = await _printerRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<PrinterDTO>(printer);
    }

    public async Task<IEnumerable<PrinterDTO>> GetByName(string name, CancellationToken cancellationToken)
    {
        var printers = await _printerRepository.GetByName(name, cancellationToken);
        return _mapper.Map<IEnumerable<PrinterDTO>>(printers);
    }

    public async Task UpdateAsync(Guid id, PrinterDTO entityDTO, CancellationToken cancellationToken)
    {
        entityDTO.PrinterId = id;
        Printers printer = _mapper.Map<Printers>(entityDTO);
        printer.LastModified = DateTime.UtcNow;
        await _printerRepository.UpdateAsync(printer, cancellationToken);
    }
}

