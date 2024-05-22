using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeFrame;
using PhotoboothBranchService.Application.Services.ThemeFrameServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeFrameController : ControllerBase
    {
        private readonly IThemeFrameService _themeFrameService;

        public ThemeFrameController(IThemeFrameService themeFrameService)
        {
            _themeFrameService = themeFrameService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateThemeFrame(CreateThemeFrameRequest createThemeFrameRequest)
        {
            try
            {
                var id = await _themeFrameService.CreateAsync(createThemeFrameRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the themeFrame: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeFrameResponse>>> GetAllThemeFrames()
        {
            try
            {
                var themeFrames = await _themeFrameService.GetAllAsync();
                return Ok(themeFrames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeFrames: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ThemeFrameResponse>>> GetPagingThemeFrames(
            [FromQuery] ThemeFrameFilter themeFrameFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var themeFrames = await _themeFrameService.GetAllPagingAsync(themeFrameFilter, pagingModel);
                return Ok(themeFrames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeFrames: {ex.Message}");
            }
        }

        // Read by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ThemeFrameResponse>>> GetThemeFramesByName(string name)
        {
            try
            {
                var themeFrames = await _themeFrameService.GetByName(name);
                return Ok(themeFrames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeFrames by name: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemeFrameResponse>> GetThemeFrameById(Guid id)
        {
            try
            {
                var themeFrame = await _themeFrameService.GetByIdAsync(id);
                if (themeFrame == null)
                {
                    return NotFound();
                }
                return Ok(themeFrame);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the themeFrame by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateThemeFrame(Guid id, UpdateThemeFrameRequest updateThemeFrameRequest)
        {
            try
            {
                await _themeFrameService.UpdateAsync(id, updateThemeFrameRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the themeFrame: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteThemeFrame(Guid id)
        {
            try
            {
                await _themeFrameService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the themeFrame: {ex.Message}");
            }
        }
    }
}
