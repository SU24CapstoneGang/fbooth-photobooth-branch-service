using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Frame;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Application.Services.FrameServices;

namespace PhotoboothBranchService.Api.Controllers;

public class FrameController : ControllerBaseApi
{
    private readonly IFrameService _frameService;

    public FrameController(IFrameService frameService)
    {
        _frameService = frameService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateFrame(CreateFrameRequest createFrameRequest)
    {
        try
        {
            var id = await _frameService.CreateAsync(createFrameRequest);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the frame: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FrameResponse>>> GetAllFrames()
    {
        try
        {
            var frames = await _frameService.GetAllAsync();
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving frames: {ex.Message}");
        }
    }

    // Read with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<FrameResponse>>> GetPagingFrames(
        [FromQuery] FrameFilter frameFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var frames = await _frameService.GetAllPagingAsync(frameFilter, pagingModel);
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving frames: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<FrameResponse>>> GetFramesByName(string name)
    {
        try
        {
            var frames = await _frameService.GetByName(name);
            return Ok(frames);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving frames by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FrameResponse>> GetFrameById(Guid id)
    {
        try
        {
            var frame = await _frameService.GetByIdAsync(id);
            if (frame == null)
            {
                return NotFound();
            }
            return Ok(frame);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the frame by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFrame(Guid id, UpdateFrameRequest updateFrameRequest)
    {
        try
        {
            await _frameService.UpdateAsync(id, updateFrameRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the frame: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFrame(Guid id)
    {
        try
        {
            await _frameService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the frame: {ex.Message}");
        }
    }
    [HttpPost("add-frame-cloud")]
    public async Task<ActionResult<PhotoResponse>> AddPhoto(IFormFile file, [FromQuery] CreateFrameRequest createFrameRequest)
    {
        try
        {

            var result = await _frameService.CreateFrameAsync(file, createFrameRequest);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding the photo: {ex.Message}");
        }
    }
}
