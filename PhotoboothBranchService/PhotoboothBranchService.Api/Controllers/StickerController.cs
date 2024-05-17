using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
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
    public async Task<ActionResult<Guid>> CreateSticker(StickerDTO stickerDTO)
    {
        try
        {
            var id = await _stickerService.CreateAsync(stickerDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the sticker: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StickerDTO>>> GetAllStickers()
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

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<StickerDTO>>> GetStickersByName(string name)
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
    public async Task<ActionResult<StickerDTO>> GetStickerById(Guid id)
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
    public async Task<ActionResult> UpdateSticker(Guid id, StickerDTO stickerDTO)
    {
        try
        {
            await _stickerService.UpdateAsync(id, stickerDTO);
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
}
