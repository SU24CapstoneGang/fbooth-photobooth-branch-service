// PrintersController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers;

public class PrintersController : ControllerBaseApi
{
    private readonly IPrinterService _printerService;

    public PrintersController(IPrinterService printerService)
    {
        _printerService = printerService;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreatePrinter(PrinterDTO printerDTO, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _printerService.CreateAsync(printerDTO, cancellationToken);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the printer: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrinterDTO>>> GetAllPrinters(CancellationToken cancellationToken)
    {
        try
        {
            var printers = await _printerService.GetAllAsync(cancellationToken);
            return Ok(printers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving printers: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<PrinterDTO>>> GetPrintersByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var printers = await _printerService.GetByName(name, cancellationToken);
            return Ok(printers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving printers by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrinterDTO>> GetPrinterById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var printer = await _printerService.GetByIdAsync(id, cancellationToken);
            if (printer == null)
            {
                return NotFound();
            }
            return Ok(printer);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the printer by ID: {ex.Message}");
        }
    }

    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrinter(Guid id, PrinterDTO printerDTO, CancellationToken cancellationToken)
    {
        try
        {
            await _printerService.UpdateAsync(id, printerDTO, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the printer: {ex.Message}");
        }
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePrinter(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _printerService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the printer: {ex.Message}");
        }
    }
}

