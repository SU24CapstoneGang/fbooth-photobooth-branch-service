using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Api.Controllers;

// PrintersController.cs
[ApiController]
[Route("api/[controller]")]
public class PrintersController : ControllerBase
{
    private readonly IPrintersRepository _printersRepository;

    public PrintersController(IPrintersRepository printersRepository)
    {
        _printersRepository = printersRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Printers>>> GetAllPrinters(CancellationToken cancellationToken)
    {
        var printers = await _printersRepository.GetAll(cancellationToken);
        return Ok(printers);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<Printers>>> GetPrintersByName(string name, CancellationToken cancellationToken)
    {
        var printers = await _printersRepository.GetByName(name, cancellationToken);
        return Ok(printers);
    }

    [HttpPost]
    public async Task<ActionResult> CreatePrinter(Printers printer, CancellationToken cancellationToken)
    {
        await _printersRepository.AddAsync(printer, cancellationToken);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Printers>> GetPrinterById(Guid id, CancellationToken cancellationToken)
    {
        var printer = await _printersRepository.GetByIdAsync(id, cancellationToken);
        if (printer == null)
        {
            return NotFound();
        }
        return Ok(printer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePrinter(Guid id, Printers printer, CancellationToken cancellationToken)
    {
        if (id != printer.Id)
        {
            return BadRequest("Invalid ID.");
        }

        await _printersRepository.UpdateAsync(printer, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePrinter(Guid id, CancellationToken cancellationToken)
    {
        var printer = await _printersRepository.GetByIdAsync(id, cancellationToken);
        if (printer == null)
        {
            return NotFound();
        }

        await _printersRepository.RemoveAsync(printer, cancellationToken);
        return NoContent();
    }
}

