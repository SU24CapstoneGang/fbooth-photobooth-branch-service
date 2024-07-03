using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Application.Services.StickerServices;

namespace PhotoboothBranchService.Api.Controllers;

public class StickerController : ControllerBaseApi
{
    private readonly IStickerService _stickerService;

    public StickerController(IStickerService stickerService)
    {
        _stickerService = stickerService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<CreateStickerResponse>> CreateSticker(CreateStickerRequest createStickerRequest)
    {
        try
        {
            var createStickerResponse = await _stickerService.CreateAsync(createStickerRequest);
            return Ok(createStickerResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the sticker: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StickerResponse>>> GetAllStickers()
    {
        try
        {
            var stickers = await _stickerService.GetAllAsync();
            return Ok(stickers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving stickers: {ex.Message}");
        }
    }
    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<StickerResponse>>> GetAllStickers(
        [FromQuery] StickerFilter stickerFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var stickers = await _stickerService.GetAllPagingAsync(stickerFilter, pagingModel);
            return Ok(stickers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving stickers: {ex.Message}");
        }
    }
    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<StickerResponse>>> GetStickersByName(string name)
    {
        try
        {
            var stickers = await _stickerService.GetByName(name);
            return Ok(stickers);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving stickers by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StickerResponse>> GetStickerById(Guid id)
    {
        try
        {
            var sticker = await _stickerService.GetByIdAsync(id);
            if (sticker == null)
            {
                return NotFound();
            }
            return Ok(sticker);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the sticker by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSticker(Guid id, UpdateStickerRequest updateStickerRequest)
    {
        try
        {
            await _stickerService.UpdateAsync(id, updateStickerRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the sticker: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSticker(Guid id)
    {
        try
        {
            await _stickerService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the sticker: {ex.Message}");
        }
    }

    [HttpPost("add-sticker-cloud")]
    public async Task<ActionResult<StickerResponse>> AddPhoto(IFormFile file, [FromQuery] CreateStickerRequest createLayoutRequest)
    {
        try
        {

            var result = await _stickerService.CreateStickerAsync(file, createLayoutRequest);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding the photo: {ex.Message}");
        }
    }
}
