using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Application.Services.PhotoBoxServices;

namespace PhotoboothBranchService.Api.Controllers
{
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
            try
            {
                var createPhotoBoxResponse = await _photoBoxService.CreateAsync(createPhotoBoxRequest);
                return Ok(createPhotoBoxResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the photo box: {ex.Message}");
            }
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoBoxResponse>>> GetAllPhotoBoxes()
        {
            try
            {
                var photoBoxes = await _photoBoxService.GetAllAsync();
                return Ok(photoBoxes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving photo boxes: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoBoxResponse>>> GetAllPhotoBoxes(
            [FromQuery] PhotoBoxFilter photoBoxFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var photoBoxes = await _photoBoxService.GetAllPagingAsync(photoBoxFilter, pagingModel);
                return Ok(photoBoxes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving photo boxes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoBoxResponse>> GetPhotoBoxById(Guid id)
        {
            try
            {
                var photoBox = await _photoBoxService.GetByIdAsync(id);
                if (photoBox == null)
                {
                    return NotFound(new { message = $"Photo box with ID {id} not found" });
                }
                return Ok(photoBox);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the photo box by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhotoBox(Guid id, UpdatePhotoBoxRequest updatePhotoBoxRequest)
        {
            try
            {
                await _photoBoxService.UpdateAsync(id, updatePhotoBoxRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the photo box: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotoBox(Guid id)
        {
            try
            {
                await _photoBoxService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the photo box: {ex.Message}");
            }
        }
    }
}
