using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Application.Services.PhotoStickerServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [Route("api/photo-sticker")]
    public class PhotoStickerController : ControllerBaseApi
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

            var createPhotoStickerResponse = await _mapStickerService.CreateAsync(createMapStickerRequest);
            return Ok(createPhotoStickerResponse);

        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoStickerResponse>>> GetAllMapStickers()
        {

            var mapStickers = await _mapStickerService.GetAllAsync();
            return Ok(mapStickers);

        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoStickerResponse>>> GetPagingMapStickers(
            [FromQuery] PhotoStickerFilter mapStickerFilter, [FromQuery] PagingModel pagingModel)
        {

            var mapStickers = await _mapStickerService.GetAllPagingAsync(mapStickerFilter, pagingModel);
            return Ok(mapStickers);

        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoStickerResponse>> GetMapStickerById(Guid id)
        {

            var mapSticker = await _mapStickerService.GetByIdAsync(id);
            if (mapSticker == null)
            {
                return NotFound();
            }
            return Ok(mapSticker);

        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMapSticker(Guid id, UpdatePhotoStickerRequest updateMapStickerRequest)
        {
            await _mapStickerService.UpdateAsync(id, updateMapStickerRequest);
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMapSticker(Guid id)
        {
            await _mapStickerService.DeleteAsync(id);
            return Ok();
        }
    }
}
