using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Application.Services.PhotoServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreatePhotoResponse>> CreatePhoto(CreatePhotoRequest createPhotoRequest)
        {

            var createPhotoResponse = await _photoService.CreateAsync(createPhotoRequest);
            return Ok(createPhotoResponse);

        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoResponse>>> GetAllPhotos()
        {

            var Photos = await _photoService.GetAllAsync();
            return Ok(Photos);

        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoResponse>>> GetPagingPhotos(
            [FromQuery] PhotoFilter PhotoFilter, [FromQuery] PagingModel pagingModel)
        {

            var Photos = await _photoService.GetAllPagingAsync(PhotoFilter, pagingModel);
            return Ok(Photos);

        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoResponse>> GetPhotoById(Guid id)
        {

            var Photo = await _photoService.GetByIdAsync(id);
            if (Photo == null)
            {
                return NotFound();
            }
            return Ok(Photo);

        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhoto(Guid id, [FromQuery] UpdatePhotoRequest updatePhotoRequest)
        {

            await _photoService.UpdateAsync(id, updatePhotoRequest);
            return Ok();

        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhoto(Guid id)
        {
            await _photoService.DeleteAsync(id);
            return Ok();
        }


        [HttpPost("add-photo-cloud")]
        public async Task<ActionResult<PhotoResponse>> AddPhoto(IFormFile file, [FromQuery] CreatePhotoRequest createPhotoRequest)
        {
            var result = await _photoService.CreatePhotoAsync(file, createPhotoRequest);
            return Ok(result);
        }
    }
}