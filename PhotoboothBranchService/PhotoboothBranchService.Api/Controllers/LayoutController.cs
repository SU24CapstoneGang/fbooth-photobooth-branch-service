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

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LayoutResponse>>> GetAllLayouts()
    {
        var layouts = await _layoutService.GetAllAsync();
        return Ok(layouts);
    }

    // gat all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<LayoutResponse>>> GetAllLayouts(
        [FromQuery] LayoutFilter layoutFilter, [FromQuery] PagingModel pagingModel)
    {
        var layouts = await _layoutService.GetAllPagingAsync(layoutFilter, pagingModel);
        return Ok(layouts);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LayoutResponse>> GetLayoutById(Guid id)
    {
        var layout = await _layoutService.GetByIdAsync(id);
        if (layout == null)
        {
            return NotFound();
        }
        return Ok(layout);

    }

    // Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLayout(IFormFile file, Guid id, UpdateLayoutRequest updateLayoutRequest)
    {
        await _layoutService.UpdateLayoutAsync(file, id, updateLayoutRequest);
        return Ok();
    }

    // Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLayout(Guid id)
    {
        await _layoutService.DeleteAsync(id);
        return Ok();
    }

    [HttpPost("add-layout-auto")]
    public async Task<ActionResult<LayoutResponse>> AddLayout(IFormFile file)
    {
        var result = await _layoutService.CreateLayoutAuto(file);
        return Ok(result);
    }
}
