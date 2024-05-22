using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeSticker;
using PhotoboothBranchService.Application.Services.ThemeStickerServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeStickerController : ControllerBase
    {
        private readonly IThemeStickerService _themeStickerService;

        public ThemeStickerController(IThemeStickerService themeStickerService)
        {
            _themeStickerService = themeStickerService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateThemeSticker(CreateThemeStickerRequest createThemeStickerRequest)
        {
            try
            {
                var id = await _themeStickerService.CreateAsync(createThemeStickerRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the themeSticker: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeStickerResponse>>> GetAllThemeStickers()
        {
            try
            {
                var themeStickers = await _themeStickerService.GetAllAsync();
                return Ok(themeStickers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeStickers: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ThemeStickerResponse>>> GetPagingThemeStickers(
            [FromQuery] ThemeStickerFilter themeStickerFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var themeStickers = await _themeStickerService.GetAllPagingAsync(themeStickerFilter, pagingModel);
                return Ok(themeStickers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeStickers: {ex.Message}");
            }
        }

        // Read by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ThemeStickerResponse>>> GetThemeStickersByName(string name)
        {
            try
            {
                var themeStickers = await _themeStickerService.GetByName(name);
                return Ok(themeStickers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeStickers by name: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemeStickerResponse>> GetThemeStickerById(Guid id)
        {
            try
            {
                var themeSticker = await _themeStickerService.GetByIdAsync(id);
                if (themeSticker == null)
                {
                    return NotFound();
                }
                return Ok(themeSticker);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the themeSticker by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateThemeSticker(Guid id, UpdateThemeStickerRequest updateThemeStickerRequest)
        {
            try
            {
                await _themeStickerService.UpdateAsync(id, updateThemeStickerRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the themeSticker: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteThemeSticker(Guid id)
        {
            try
            {
                await _themeStickerService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the themeSticker: {ex.Message}");
            }
        }
    }
}
