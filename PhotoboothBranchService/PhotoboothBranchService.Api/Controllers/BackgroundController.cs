using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Application.Services.BackgroundServices;

namespace PhotoboothBranchService.Api.Controllers;

public class BackgroundController : ControllerBaseApi
{
    private readonly IBackgroundService _backgroundService;

    public BackgroundController(IBackgroundService backgroundService)
    {
        _backgroundService = backgroundService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<CreateBackgroundResponse>> CreateBackground(CreateBackgroundRequest createBackgroundRequest)
    {
        try
        {
            var createBackgroundResponse = await _backgroundService.CreateAsync(createBackgroundRequest);
            return Ok(createBackgroundResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the background: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BackgroundResponse>>> GetAllBackgrounds()
    {
        try
        {
            var frames = await _backgroundService.GetAllAsync();
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving background: {ex.Message}");
        }
    }

    // Read with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<BackgroundResponse>>> GetPagingBackgrounds(
        [FromQuery] BackgroundFilter backgroundFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var frames = await _backgroundService.GetAllPagingAsync(backgroundFilter, pagingModel);
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving background: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<BackgroundResponse>>> GetBackgroundByName(string name)
    {
        try
        {
            var frames = await _backgroundService.GetByName(name);
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving background by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BackgroundResponse>> GetBackgroundById(Guid id)
    {
        try
        {
            var frame = await _backgroundService.GetByIdAsync(id);
            if (frame == null)
            {
                return NotFound();
            }
            return Ok(frame);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the background by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBackground(Guid id, UpdateBackgroundRequest updateBackgroundRequest)
    {
        try
        {
            await _backgroundService.UpdateAsync(id, updateBackgroundRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the background: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFrame(Guid id)
    {
        try
        {
            await _backgroundService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the background: {ex.Message}");
        }
    }
    [HttpPost("add-frame-cloud")]
    public async Task<ActionResult<BackgroundResponse>> AddBackground(IFormFile file, [FromQuery] CreateBackgroundRequest createBackgroundRequest)
    {
        try
        {

            var result = await _backgroundService.CreateBackgroundAsync(file, createBackgroundRequest);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding the background: {ex.Message}");
        }
    }
}
