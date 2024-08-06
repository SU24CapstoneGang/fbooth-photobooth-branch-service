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


    // Read
    [HttpGet("admin")]
    public async Task<ActionResult<IEnumerable<BackgroundResponse>>> GetAllBackgrounds()
    {
        var background = await _backgroundService.GetAllAsync();
        return Ok(background);
    }

    [HttpGet("customer")]
    public async Task<ActionResult<IEnumerable<BackgroundResponse>>> GetBackgrounds()
    {
        var background = await _backgroundService.GetAvailableAsync();
        return Ok(background);
    }

    // Read with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<BackgroundResponse>>> GetPagingBackgrounds(
        [FromQuery] BackgroundFilter backgroundFilter, [FromQuery] PagingModel pagingModel)
    {
        var frames = await _backgroundService.GetAllPagingAsync(backgroundFilter, pagingModel);
        return Ok(frames);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<BackgroundResponse>>> GetBackgroundByName(string name)
    {

        var frames = await _backgroundService.GetByName(name);
        return Ok(frames);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BackgroundResponse>> GetBackgroundById(Guid id)
    {
        var frame = await _backgroundService.GetByIdAsync(id);
        if (frame == null)
        {
            return NotFound();
        }
        return Ok(frame);
    }

    // Update
    [HttpPut("{backGroundID}")]
    public async Task<ActionResult> UpdateBackground(IFormFile file, Guid backGroundID, [FromQuery] UpdateBackgroundRequest updateBackgroundRequest)
    {
        await _backgroundService.UpdateBackGroundAsync(file, backGroundID, updateBackgroundRequest);
        return Ok();
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFrame(Guid id)
    {
        await _backgroundService.DeleteAsync(id);
        return Ok();
    }
    [HttpPost("add-background-cloud")]
    public async Task<ActionResult<BackgroundResponse>> AddBackground(IFormFile file, Guid layoutID)
    {
        var result = await _backgroundService.CreateBackgroundAsync(file, layoutID);
        return Ok(result);
    }
}
