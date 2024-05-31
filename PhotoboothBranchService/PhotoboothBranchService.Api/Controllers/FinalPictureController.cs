using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
using PhotoboothBranchService.Application.Services.FinalPictureServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinalPictureController : ControllerBase
    {
        private readonly IFinalPictureService _finalPictureService;
        private readonly ICloudinaryService _cloudinaryService;

        public FinalPictureController(IFinalPictureService finalPictureService, ICloudinaryService cloudinaryService)
        {
            _finalPictureService = finalPictureService;
            _cloudinaryService = cloudinaryService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateFinalPicture(CreateFinalPictureRequest createFinalPictureRequest)
        {
            try
            {
                var id = await _finalPictureService.CreateAsync(createFinalPictureRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the finalPicture: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinalPictureResponse>>> GetAllFinalPictures()
        {
            try
            {
                var finalPictures = await _finalPictureService.GetAllAsync();
                return Ok(finalPictures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving finalPictures: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<FinalPictureResponse>>> GetPagingFinalPictures(
            [FromQuery] FinalPictureFilter finalPictureFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var finalPictures = await _finalPictureService.GetAllPagingAsync(finalPictureFilter, pagingModel);
                return Ok(finalPictures);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving finalPictures: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<FinalPictureResponse>> GetFinalPictureById(Guid id)
        {
            try
            {
                var finalPicture = await _finalPictureService.GetByIdAsync(id);
                if (finalPicture == null)
                {
                    return NotFound();
                }
                return Ok(finalPicture);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the finalPicture by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFinalPicture(Guid id, UpdateFinalPictureRequest updateFinalPictureRequest)
        {
            try
            {
                await _finalPictureService.UpdateAsync(id, updateFinalPictureRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the finalPicture: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFinalPicture(Guid id)
        {
            try
            {
                await _finalPictureService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the finalPicture: {ex.Message}");
            }
        }


        [HttpPost("add-photo-cloud")]
        public async Task<ActionResult<FinalPictureResponse>> AddPhoto(IFormFile file, Guid branchID, int photoTaken, Guid layoutID, string? discountCode, Guid? accountID)
        {
            try
            {

                var result = await _finalPictureService.CreateFinalPictureAsync(file, branchID, photoTaken, layoutID, discountCode, accountID);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the photo: {ex.Message}");
            }
        }
    }
}
