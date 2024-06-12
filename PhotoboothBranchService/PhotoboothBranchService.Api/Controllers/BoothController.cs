using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Application.Services.BoothServices;

namespace PhotoboothBranchService.Api.Controllers;

public class BoothController : ControllerBaseApi
{
    private readonly IBoothService _boothService;

    public BoothController(IBoothService boothService)
    {
        _boothService = boothService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateBooth(CreateBoothRequest createBoothRequest)
    {
        try
        {
            var id = await _boothService.CreateAsync(createBoothRequest);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the booth: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> GetAllBooths()
    {
        try
        {
            var booths = await _boothService.GetAllAsync();
            return Ok(booths);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving booths: {ex.Message}");
        }
    }

    // Read with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> GetAllBooths(
        [FromQuery] BoothFilter boothFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var booths = await _boothService.GetAllPagingAsync(boothFilter, pagingModel);
            return Ok(booths);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving booths: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<BoothResponse>>> GetBoothsByName(string name)
    {
        try
        {
            var booths = await _boothService.GetByName(name);
            return Ok(booths);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving booths by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoothResponse>> GetBoothById(Guid id)
    {
        try
        {
            var booth = await _boothService.GetByIdAsync(id);
            if (booth == null)
            {
                return NotFound();
            }
            return Ok(booth);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the booth by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBooth(Guid id, UpdateBoothRequest updateBoothRequest)
    {
        try
        {
            await _boothService.UpdateAsync(id, updateBoothRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the booth: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBooth(Guid id)
    {
        try
        {
            await _boothService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the booth: {ex.Message}");
        }
    }
}
