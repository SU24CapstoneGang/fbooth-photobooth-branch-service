using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Application.Services.SessionPackageServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class SessionPackageController : ControllerBaseApi
    {
        private readonly ISessionPackageService _sessionPackageService;

        public SessionPackageController(ISessionPackageService sessionPackageService)
        {
            _sessionPackageService = sessionPackageService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreateSessionPackageResponse>> CreateSessionPackage(CreateSessionPackageRequest createSessionPackageRequest)
        {
            var createSessionPackageResponse = await _sessionPackageService.CreateAsync(createSessionPackageRequest);
            return Ok(createSessionPackageResponse);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionPackageResponse>>> GetAllSessionPackages()
        {
            var sessionPackages = await _sessionPackageService.GetAllAsync();
            return Ok(sessionPackages);
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<SessionPackageResponse>>> GetAllSessionPackages(
            [FromQuery] SessionPackageFilter sessionPackageFilter, [FromQuery] PagingModel pagingModel)
        {
            var sessionPackages = await _sessionPackageService.GetAllPagingAsync(sessionPackageFilter, pagingModel);
            return Ok(sessionPackages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionPackageResponse>> GetSessionPackageById(Guid id)
        {
            var sessionPackage = await _sessionPackageService.GetByIdAsync(id);
            if (sessionPackage == null)
            {
                return NotFound();
            }
            return Ok(sessionPackage);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSessionPackage(Guid id, UpdateSessionPackageRequest updateSessionPackageRequest)
        {
            await _sessionPackageService.UpdateAsync(id, updateSessionPackageRequest);
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSessionPackage(Guid id)
        {
            await _sessionPackageService.DeleteAsync(id);
            return Ok();
        }
    }
}
