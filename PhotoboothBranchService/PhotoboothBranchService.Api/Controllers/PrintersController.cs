// PrintersController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.Services.PrinterServices;
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
    public async Task<ActionResult<Guid>> CreatePrinter(PrinterDTO printerDTO)
    {
        try
        {
            var id = await _printerService.CreateAsync(printerDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the printer: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrinterDTO>>> GetAllPrinters()
    {
        try
        {
            var printers = await _printerService.GetAllAsync();
            return Ok(printers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving printers: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<PrinterDTO>>> GetPrintersByName(string name)
    {
        try
        {
            var printers = await _printerService.GetByName(name);
            return Ok(printers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving printers by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrinterDTO>> GetPrinterById(Guid id)
    {
        try
        {
            var printer = await _printerService.GetByIdAsync(id);
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
    public async Task<ActionResult> UpdatePrinter(Guid id, PrinterDTO printerDTO)
    {
        try
        {
            await _printerService.UpdateAsync(id, printerDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the printer: {ex.Message}");
        }
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePrinter(Guid id)
    {
        try
        {
            await _printerService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the printer: {ex.Message}");
        }
    }
}

