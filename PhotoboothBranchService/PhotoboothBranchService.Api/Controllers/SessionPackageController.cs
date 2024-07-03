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
            try
            {
                var createSessionPackageResponse = await _sessionPackageService.CreateAsync(createSessionPackageRequest);
                return Ok(createSessionPackageResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the session package: {ex.Message}");
            }
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionPackageResponse>>> GetAllSessionPackages()
        {
            try
            {
                var sessionPackages = await _sessionPackageService.GetAllAsync();
                return Ok(sessionPackages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving session packages: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<SessionPackageResponse>>> GetAllSessionPackages(
            [FromQuery] SessionPackageFilter sessionPackageFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var sessionPackages = await _sessionPackageService.GetAllPagingAsync(sessionPackageFilter, pagingModel);
                return Ok(sessionPackages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving session packages: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessionPackageResponse>> GetSessionPackageById(Guid id)
        {
            try
            {
                var sessionPackage = await _sessionPackageService.GetByIdAsync(id);
                if (sessionPackage == null)
                {
                    return NotFound();
                }
                return Ok(sessionPackage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the session package by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSessionPackage(Guid id, UpdateSessionPackageRequest updateSessionPackageRequest)
        {
            try
            {
                await _sessionPackageService.UpdateAsync(id, updateSessionPackageRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the session package: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSessionPackage(Guid id)
        {
            try
            {
                await _sessionPackageService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the session package: {ex.Message}");
            }
        }
    }
}
