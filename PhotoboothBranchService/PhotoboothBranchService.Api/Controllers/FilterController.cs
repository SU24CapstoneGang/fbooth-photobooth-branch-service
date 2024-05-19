using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels.Common;
using PhotoboothBranchService.Application.DTOs.RequestModels.Filter;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Filter;
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
    public async Task<ActionResult<Guid>> CreateFilter(CreateFilterRequest createFilterRequest)
    {
        try
        {
            var id = await _filterService.CreateAsync(createFilterRequest);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the filter: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Filterresponse>>> GetAllFilters()
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

    // Read with paging and filter
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<Filterresponse>>> GetAllFilters(
        [FromBody] FilterPagingModel<FilterFilter> filterPagingModel)
    {
        try
        {
            var filters = await _filterService.GetAllPagingAsync(filterPagingModel.Filter,filterPagingModel.Paging);
            return Ok(filters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving filters: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<Filterresponse>>> GetFiltersByName(string name)
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
    public async Task<ActionResult<Filterresponse>> GetFilterById(Guid id)
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
    public async Task<ActionResult> UpdateFilter(Guid id, UpdateFilterRequest updateFilterRequest)
    {
        try
        {
            await _filterService.UpdateAsync(id, updateFilterRequest);
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

