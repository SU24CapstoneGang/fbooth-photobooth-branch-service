using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Services.FilterServices;

namespace PhotoboothBranchService.Api.Controllers;

public class FilterController : ControllerBaseApi
{
    private readonly IFilterService _filterService;

    public FilterController(IFilterService filterService)
    {
        _filterService = filterService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateFilter(FilterDTO filterDTO)
    {
        try
        {
            var id = await _filterService.CreateAsync(filterDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the filter: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilterDTO>>> GetAllFilters()
    {
        try
        {
            var filters = await _filterService.GetAllAsync();
            return Ok(filters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving filters: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<FilterDTO>>> GetFiltersByName(string name)
    {
        try
        {
            var filters = await _filterService.GetByName(name);
            return Ok(filters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving filters by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilterDTO>> GetFilterById(Guid id)
    {
        try
        {
            var filter = await _filterService.GetByIdAsync(id);
            if (filter == null)
            {
                return NotFound();
            }
            return Ok(filter);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the filter by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFilter(Guid id, FilterDTO filterDTO)
    {
        try
        {
            await _filterService.UpdateAsync(id, filterDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the filter: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFilter(Guid id)
    {
        try
        {
            await _filterService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the filter: {ex.Message}");
        }
    }
}

