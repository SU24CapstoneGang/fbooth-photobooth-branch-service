using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;

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
    public async Task<ActionResult<Guid>> CreateLayout(LayoutDTO layoutDTO)
    {
        try
        {
            var id = await _layoutService.CreateAsync(layoutDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the layout: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LayoutDTO>>> GetAllLayouts()
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

    [HttpGet("{id}")]
    public async Task<ActionResult<LayoutDTO>> GetLayoutById(Guid id)
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
    public async Task<ActionResult> UpdateLayout(Guid id, LayoutDTO layoutDTO)
    {
        try
        {
            await _layoutService.UpdateAsync(id, layoutDTO);
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
}
