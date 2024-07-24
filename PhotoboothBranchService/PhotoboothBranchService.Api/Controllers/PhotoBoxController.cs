using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Application.Services.PhotoBoxServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [Route("api/photo-box")]
    public class PhotoBoxController : ControllerBaseApi
    {
        private readonly IPhotoBoxService _photoBoxService;

        public PhotoBoxController(IPhotoBoxService photoBoxService)
        {
            _photoBoxService = photoBoxService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreatePhotoBoxResponse>> CreatePhotoBox(CreatePhotoBoxRequest createPhotoBoxRequest)
        {

            var createPhotoBoxResponse = await _photoBoxService.CreateAsync(createPhotoBoxRequest);
            return Ok(createPhotoBoxResponse);

        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoBoxResponse>>> GetAllPhotoBoxes()
        {

            var photoBoxes = await _photoBoxService.GetAllAsync();
            return Ok(photoBoxes);

        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoBoxResponse>>> GetAllPhotoBoxes(
            [FromQuery] PhotoBoxFilter photoBoxFilter, [FromQuery] PagingModel pagingModel)
        {

            var photoBoxes = await _photoBoxService.GetAllPagingAsync(photoBoxFilter, pagingModel);
            return Ok(photoBoxes);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoBoxResponse>> GetPhotoBoxById(Guid id)
        {

            var photoBox = await _photoBoxService.GetByIdAsync(id);
            if (photoBox == null)
            {
                return NotFound(new { message = $"Photo box with ID {id} not found" });
            }
            return Ok(photoBox);

        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhotoBox(Guid id, UpdatePhotoBoxRequest updatePhotoBoxRequest)
        {

            await _photoBoxService.UpdateAsync(id, updatePhotoBoxRequest);
            return Ok();

        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotoBox(Guid id)
        {

            await _photoBoxService.DeleteAsync(id);
            return Ok();

        }
    }
}
