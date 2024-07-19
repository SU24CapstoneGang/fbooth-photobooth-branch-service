// PhotoBoothBranchesController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BoothBranch;
using PhotoboothBranchService.Application.Services.BoothBranchServices;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers;

public class BranchController : ControllerBaseApi
{
    private readonly IBranchService _photoBoothBranchService;

    public BranchController(IBranchService photoBoothBranchService)
    {
        _photoBoothBranchService = photoBoothBranchService;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<CreateBranchResponse>> CreateBranch(CreateBranchRequest createPhotoBoothBranchRequest)
    {
        var createBoothBranchResponse = await _photoBoothBranchService.CreateAsync(createPhotoBoothBranchRequest);
        return Ok(createBoothBranchResponse);
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetAllBranches()
    {

        var branches = await _photoBoothBranchService.GetAllAsync();
        return Ok(branches);

    }
    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetAllBranches(
        [FromQuery] BranchFilter photoBoothBranchFilter, [FromQuery] PagingModel pagingModel)
    {
        var branches = await _photoBoothBranchService.GetAllPagingAsync(photoBoothBranchFilter, pagingModel);
        return Ok(branches);

    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetBranchesByStatus(BranchStatus status)
    {

        var branches = await _photoBoothBranchService.GetByStatus(status);
        return Ok(branches);

    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetBranchesByName(string name)
    {

        var branches = await _photoBoothBranchService.SearchByName(name);
        return Ok(branches);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BranchResponse>> GetBranchById(Guid id)
    {
        var branch = await _photoBoothBranchService.GetByIdAsync(id);
        if (branch == null)
        {
            return NotFound();
        }
        return Ok(branch);
    }

    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBranch(Guid id, UpdateBranchRequest updatePhotoBoothBranchRequest)
    {
        await _photoBoothBranchService.UpdateAsync(id, updatePhotoBoothBranchRequest);
        return Ok();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBranch(Guid id)
    {
        await _photoBoothBranchService.DeleteAsync(id);
        return Ok();
    }
}

