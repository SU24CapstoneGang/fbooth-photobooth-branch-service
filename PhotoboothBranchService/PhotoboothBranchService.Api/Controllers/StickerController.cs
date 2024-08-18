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

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StickerResponse>>> GetAllStickers()
    {
        var stickers = await _stickerService.GetAllAsync();
        return Ok(stickers);
    }

    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<StickerResponse>>> GetAllStickers(
        [FromQuery] StickerFilter stickerFilter, [FromQuery] PagingModel pagingModel)
    {
        var stickers = await _stickerService.GetAllPagingAsync(stickerFilter, pagingModel);
        return Ok(stickers);
    }
    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<StickerResponse>>> GetStickersByName(string name)
    {
        var stickers = await _stickerService.GetByName(name);
        return Ok(stickers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StickerResponse>> GetStickerById(Guid id)
    {
        var sticker = await _stickerService.GetByIdAsync(id);
        if (sticker == null)
        {
            return NotFound();
        }
        return Ok(sticker);
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSticker(IFormFile file, Guid id, [FromQuery] UpdateStickerRequest updateStickerRequest)
    {
        await _stickerService.UpdateStickerAsync(file, id, updateStickerRequest);
        return Ok();
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSticker(Guid id)
    {
        await _stickerService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("add-sticker-cloud")]
    public async Task<ActionResult<StickerResponse>> AddSticker([FromForm]CreateStickerRequest request)
    {
        var result = await _stickerService.CreateStickerAsync(request);
        return Ok(result);
    }
}
