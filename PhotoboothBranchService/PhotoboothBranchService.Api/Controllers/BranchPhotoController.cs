using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.Services.BranchPhotoServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class BranchPhotoController : ControllerBaseApi
    {
        private readonly IBranchPhotoService _branchPhotoService;

        public BranchPhotoController(IBranchPhotoService branchPhotoService)
        {
            _branchPhotoService = branchPhotoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _branchPhotoService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _branchPhotoService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _branchPhotoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
