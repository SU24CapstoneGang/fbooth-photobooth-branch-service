// PrintersController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Printer;
using PhotoboothBranchService.Application.Services.PrinterServices;

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
    public async Task<ActionResult<Guid>> CreatePrinter(CreatePrinterRequest createPrinterRequest)
    {
        try
        {
            var id = await _printerService.CreateAsync(createPrinterRequest);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the printer: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrinterResponse>>> GetAllPrinters()
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
    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<PrinterResponse>>> GetAllPrinters(
        [FromQuery] PrinterFilter printerFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var printers = await _printerService.GetAllPagingAsync(printerFilter, pagingModel);
            return Ok(printers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving printers: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<PrinterResponse>>> GetPrintersByName(string name)
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
    public async Task<ActionResult<PrinterResponse>> GetPrinterById(Guid id)
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
    public async Task<ActionResult> UpdatePrinter(Guid id, UpdatePrinterRequest updatePrinterRequest)
    {
        try
        {
            await _printerService.UpdateAsync(id, updatePrinterRequest);
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

