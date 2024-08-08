using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.Services.BoothPhotoServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class BoothPhotoController : ControllerBaseApi
    {
        private readonly IBoothPhotoService _boothPhotoServices;

        public BoothPhotoController(IBoothPhotoService boothPhotoServices )
        {
            _boothPhotoServices = boothPhotoServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _boothPhotoServices.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _boothPhotoServices.GetByIdAsync(id);
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
                await _boothPhotoServices.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
