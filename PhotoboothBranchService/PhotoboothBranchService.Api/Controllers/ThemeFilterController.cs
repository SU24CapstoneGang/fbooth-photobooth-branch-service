using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ThemeFilter;
using PhotoboothBranchService.Application.Services.ThemeFilterServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeFilterController : ControllerBase
    {
        private readonly IThemeFilterService _themeFilterService;

        public ThemeFilterController(IThemeFilterService themeFilterService)
        {
            _themeFilterService = themeFilterService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateThemeFilter(CreateThemeFilterRequest createThemeFilterRequest)
        {
            try
            {
                var id = await _themeFilterService.CreateAsync(createThemeFilterRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the themeFilter: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeFilterResponse>>> GetAllThemeFilters()
        {
            try
            {
                var themeFilters = await _themeFilterService.GetAllAsync();
                return Ok(themeFilters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeFilters: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ThemeFilterResponse>>> GetPagingThemeFilters(
            [FromQuery] ThemeFilterFilter themeFilterFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var themeFilters = await _themeFilterService.GetAllPagingAsync(themeFilterFilter, pagingModel);
                return Ok(themeFilters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeFilters: {ex.Message}");
            }
        }

        // Read by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ThemeFilterResponse>>> GetThemeFiltersByName(string name)
        {
            try
            {
                var themeFilters = await _themeFilterService.GetByName(name);
                return Ok(themeFilters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving themeFilters by name: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemeFilterResponse>> GetThemeFilterById(Guid id)
        {
            try
            {
                var themeFilter = await _themeFilterService.GetByIdAsync(id);
                if (themeFilter == null)
                {
                    return NotFound();
                }
                return Ok(themeFilter);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the themeFilter by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateThemeFilter(Guid id, UpdateThemeFilterRequest updateThemeFilterRequest)
        {
            try
            {
                await _themeFilterService.UpdateAsync(id, updateThemeFilterRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the themeFilter: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteThemeFilter(Guid id)
        {
            try
            {
                await _themeFilterService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the themeFilter: {ex.Message}");
            }
        }
    }
}
