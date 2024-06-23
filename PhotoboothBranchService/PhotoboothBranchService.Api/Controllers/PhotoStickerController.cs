using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Application.Services.PhotoStickerServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoStickerController : ControllerBase
    {
        private readonly IPhotoStickerService _mapStickerService;

        public PhotoStickerController(IPhotoStickerService mapStickerService)
        {
            _mapStickerService = mapStickerService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreatePhotoStickerResponse>> CreateMapSticker(CreatePhotoStickerRequest createMapStickerRequest)
        {
            try
            {
                var createPhotoStickerResponse = await _mapStickerService.CreateAsync(createMapStickerRequest);
                return Ok(createPhotoStickerResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the mapSticker: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoStickerResponse>>> GetAllMapStickers()
        {
            try
            {
                var mapStickers = await _mapStickerService.GetAllAsync();
                return Ok(mapStickers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving mapStickers: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoStickerResponse>>> GetPagingMapStickers(
            [FromQuery] PhotoStickerFilter mapStickerFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var mapStickers = await _mapStickerService.GetAllPagingAsync(mapStickerFilter, pagingModel);
                return Ok(mapStickers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving mapStickers: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoStickerResponse>> GetMapStickerById(Guid id)
        {
            try
            {
                var mapSticker = await _mapStickerService.GetByIdAsync(id);
                if (mapSticker == null)
                {
                    return NotFound();
                }
                return Ok(mapSticker);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the mapSticker by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMapSticker(Guid id, UpdatePhotoStickerRequest updateMapStickerRequest)
        {
            try
            {
                await _mapStickerService.UpdateAsync(id, updateMapStickerRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the mapSticker: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMapSticker(Guid id)
        {
            try
            {
                await _mapStickerService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the mapSticker: {ex.Message}");
            }
        }
    }
}
