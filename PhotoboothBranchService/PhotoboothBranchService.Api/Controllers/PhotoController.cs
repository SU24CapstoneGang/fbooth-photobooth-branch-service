using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Application.Services.PhotoServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly ICloudinaryService _cloudinaryService;

        public PhotoController(IPhotoService photoService, ICloudinaryService cloudinaryService)
        {
            _photoService = photoService;
            _cloudinaryService = cloudinaryService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePhoto(CreatePhotoRequest createPhotoRequest)
        {
            try
            {
                var id = await _photoService.CreateAsync(createPhotoRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the Photo: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoResponse>>> GetAllPhotos()
        {
            try
            {
                var Photos = await _photoService.GetAllAsync();
                return Ok(Photos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Photos: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoResponse>>> GetPagingPhotos(
            [FromQuery] PhotoFilter PhotoFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var Photos = await _photoService.GetAllPagingAsync(PhotoFilter, pagingModel);
                return Ok(Photos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Photos: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoResponse>> GetPhotoById(Guid id)
        {
            try
            {
                var Photo = await _photoService.GetByIdAsync(id);
                if (Photo == null)
                {
                    return NotFound();
                }
                return Ok(Photo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the Photo by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhoto(Guid id, UpdatePhotoRequest updatePhotoRequest)
        {
            try
            {
                await _photoService.UpdateAsync(id, updatePhotoRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the Photo: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhoto(Guid id)
        {
            try
            {
                await _photoService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Photo: {ex.Message}");
            }
        }


        [HttpPost("add-photo-cloud")]
        public async Task<ActionResult<PhotoResponse>> AddPhoto(IFormFile file, CreatePhotoRequest createPhotoRequest)
        {
            try
            {

                var result = await _photoService.CreatePhotoAsync(file, createPhotoRequest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the photo: {ex.Message}");
            }
        }
    }
}