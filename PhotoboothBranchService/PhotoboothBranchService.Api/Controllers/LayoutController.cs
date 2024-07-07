using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Layout;
using PhotoboothBranchService.Application.Services.LayoutServices;

namespace PhotoboothBranchService.Api.Controllers;

public class LayoutController : ControllerBaseApi
{
    private readonly ILayoutService _layoutService;

    public LayoutController(ILayoutService layoutService)
    {
        _layoutService = layoutService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<CreateLayoutResponse>> CreateLayout(CreateLayoutRequest createLayoutRequest)
    {
        try
        {
            var createLayoutResponse = await _layoutService.CreateAsync(createLayoutRequest);
            return Ok(createLayoutResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the layout: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LayoutResponse>>> GetAllLayouts()
    {
        try
        {
            var layouts = await _layoutService.GetAllAsync();
            return Ok(layouts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving layouts: {ex.Message}");
        }
    }

    // gat all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<LayoutResponse>>> GetAllLayouts(
        [FromQuery] LayoutFilter layoutFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var layouts = await _layoutService.GetAllPagingAsync(layoutFilter, pagingModel);
            return Ok(layouts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving layouts: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LayoutResponse>> GetLayoutById(Guid id)
    {
        try
        {
            var layout = await _layoutService.GetByIdAsync(id);
            if (layout == null)
            {
                return NotFound();
            }
            return Ok(layout);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the layout by ID: {ex.Message}");
        }
    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLayout(Guid id, UpdateLayoutRequest updateLayoutRequest)
    {
        try
        {
            await _layoutService.UpdateAsync(id, updateLayoutRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the layout: {ex.Message}");
        }
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLayout(Guid id)
    {
        try
        {
            await _layoutService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the layout: {ex.Message}");
        }
    }

    [HttpPost("add-layout-cloud")]
    public async Task<ActionResult<LayoutResponse>> AddPhoto(IFormFile file, [FromQuery] CreateLayoutRequest createLayoutRequest)
    {
        try
        {

            var result = await _layoutService.CreateLayoutAsync(file, createLayoutRequest);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding the photo: {ex.Message}");
        }
    }

    [HttpPost("add-layout-auto")]
    public async Task<ActionResult<LayoutResponse>> AddLayout(IFormFile file)
    {
        try
        {

            var result = await _layoutService.CreateLayoutAuto(file);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while adding the photo: {ex.Message}");
        }
    }
}
