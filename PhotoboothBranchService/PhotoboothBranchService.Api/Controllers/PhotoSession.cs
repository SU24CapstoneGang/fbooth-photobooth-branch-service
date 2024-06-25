using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Application.Services.PhotoSessionServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PhotoSessionController : ControllerBaseApi
    {
        private readonly IPhotoSessionService _photoSessionService;

        public PhotoSessionController(IPhotoSessionService photoSessionService)
        {
            _photoSessionService = photoSessionService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePhotoSession(CreatePhotoSessionRequest createPhotoSessionRequest)
        {
            try
            {
                var createPhotoSessionResponse = await _photoSessionService.CreateAsync(createPhotoSessionRequest);
                return Ok(createPhotoSessionResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the photo session: {ex.Message}");
            }
        }

        //validate code
        [HttpPost("validate")]
        public async Task<bool> ValidateSession(ValidateSessionPhotoRequest validateSessionPhotoRequest)
        {
            return await _photoSessionService.ValidatePhotoSession(validateSessionPhotoRequest);
        }
        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoSessionResponse>>> GetAllPhotoSessions()
        {
            try
            {
                var photoSessions = await _photoSessionService.GetAllAsync();
                return Ok(photoSessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving photo sessions: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoSessionResponse>>> GetAllPhotoSessions(
            [FromQuery] PhotoSessionFilter photoSessionFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var photoSessions = await _photoSessionService.GetAllPagingAsync(photoSessionFilter, pagingModel);
                return Ok(photoSessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving photo sessions: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoSessionResponse>> GetPhotoSessionById(Guid id)
        {
            try
            {
                var photoSession = await _photoSessionService.GetByIdAsync(id);
                if (photoSession == null)
                {
                    return NotFound();
                }
                return Ok(photoSession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the photo session by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhotoSession(Guid id, UpdatePhotoSessionRequest updatePhotoSessionRequest)
        {
            try
            {
                await _photoSessionService.UpdateAsync(id, updatePhotoSessionRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the photo session: {ex.Message}");
            }
        }


        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotoSession(Guid id)
        {
            try
            {
                await _photoSessionService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the photo session: {ex.Message}");
            }
        }
    }
}
