using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Application.Services.PhotoSessionServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [Route("api/photo-session")]
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

            var createPhotoSessionResponse = await _photoSessionService.CreateAsync(createPhotoSessionRequest);
            return Ok(createPhotoSessionResponse);

        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoSessionResponse>>> GetAllPhotoSessions()
        {

            var photoSessions = await _photoSessionService.GetAllAsync();
            return Ok(photoSessions);

        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PhotoSessionResponse>>> GetAllPhotoSessions(
            [FromQuery] PhotoSessionFilter photoSessionFilter, [FromQuery] PagingModel pagingModel)
        {

            var photoSessions = await _photoSessionService.GetAllPagingAsync(photoSessionFilter, pagingModel);
            return Ok(photoSessions);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoSessionResponse>> GetPhotoSessionById(Guid id)
        {

            var photoSession = await _photoSessionService.GetByIdAsync(id);
            if (photoSession == null)
            {
                return NotFound();
            }
            return Ok(photoSession);

        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhotoSession(Guid id, UpdatePhotoSessionRequest updatePhotoSessionRequest)
        {

            await _photoSessionService.UpdateAsync(id, updatePhotoSessionRequest);
            return Ok();

        }


        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotoSession(Guid id)
        {

            await _photoSessionService.DeleteAsync(id);
            return Ok();

        }
    }
}
