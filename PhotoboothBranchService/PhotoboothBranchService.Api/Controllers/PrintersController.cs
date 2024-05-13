// PrintersController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PrintersController : ControllerBaseApi
    {
        private readonly IPrinterRepository _printersRepository;

        public PrintersController(IPrinterRepository printersRepository)
        {
            _printersRepository = printersRepository;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Printer>>> GetAllPrinters(CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var printers = await _printersRepository.GetAll(cancellationToken);
        //        return Ok(printers);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while retrieving printers: {ex.Message}");
        //    }
        //}

        //[HttpGet("name/{name}")]
        //public async Task<ActionResult<IEnumerable<Printer>>> GetPrintersByName(string name, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var printers = await _printersRepository.GetByName(name, cancellationToken);
        //        return Ok(printers);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while retrieving printers by name: {ex.Message}");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult> CreatePrinter(Printer printer, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        await _printersRepository.AddAsync(printer, cancellationToken);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while creating the printer: {ex.Message}");
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Printer>> GetPrinterById(Guid id, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var printer = await _printersRepository.GetByIdAsync(id, cancellationToken);
        //        if (printer == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(printer);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while retrieving the printer by ID: {ex.Message}");
        //    }
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdatePrinter(Guid id, Printer printer, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        if (id != printer.Id)
        //        {
        //            return BadRequest("Invalid ID.");
        //        }

        //        await _printersRepository.UpdateAsync(printer, cancellationToken);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while updating the printer: {ex.Message}");
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeletePrinter(Guid id, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var printer = await _printersRepository.GetByIdAsync(id, cancellationToken);
        //        if (printer == null)
        //        {
        //            return NotFound();
        //        }

        //        await _printersRepository.RemoveAsync(printer, cancellationToken);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while deleting the printer: {ex.Message}");
        //    }
        //}
    }
}
