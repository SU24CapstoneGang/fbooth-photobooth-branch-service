using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Theme;
using PhotoboothBranchService.Application.Services.ThemeServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeFrameService;

        public ThemeController(IThemeService themeFrameService)
        {
            _themeFrameService = themeFrameService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateThemeFrame(CreateThemeRequest createThemeFrameRequest)
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
        public async Task<ActionResult<IEnumerable<ThemeResponse>>> GetAllThemeFrames()
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
        public async Task<ActionResult<IEnumerable<ThemeResponse>>> GetPagingThemeFrames(
            [FromQuery] ThemeFilter themeFrameFilter, [FromQuery] PagingModel pagingModel)
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
        public async Task<ActionResult<IEnumerable<ThemeResponse>>> GetThemeFramesByName(string name)
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
        public async Task<ActionResult<ThemeResponse>> GetThemeFrameById(Guid id)
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
        public async Task<ActionResult> UpdateThemeFrame(Guid id, UpdateThemeRequest updateThemeFrameRequest)
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
