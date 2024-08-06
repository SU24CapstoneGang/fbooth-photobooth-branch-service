// PhotoBoothBranchesController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Application.Services.BranchServices;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers;

public class BranchController : ControllerBaseApi
{
    private readonly IBranchService _branchService;

    public BranchController(IBranchService branchService)
    {
        _branchService = branchService;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<CreateBranchResponse>> CreateBranch(CreateBranchRequest createPhotoBoothBranchRequest, BranchStatus status)
    {
        var createBoothBranchResponse = await _branchService.CreateAsync(createPhotoBoothBranchRequest, status);
        return Ok(createBoothBranchResponse);
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetAllBranches()
    {

        var branches = await _branchService.GetAllAsync();
        return Ok(branches);

    }
    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetAllBranches(
        [FromQuery] BranchFilter photoBoothBranchFilter, [FromQuery] PagingModel pagingModel)
    {
        var branches = await _branchService.GetAllPagingAsync(photoBoothBranchFilter, pagingModel);
        return Ok(branches);

    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetBranchesByStatus(BranchStatus status)
    {

        var branches = await _branchService.GetByStatus(status);
        return Ok(branches);

    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetBranchesByName(string name)
    {

        var branches = await _branchService.SearchByName(name);
        return Ok(branches);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BranchResponse>> GetBranchById(Guid id)
    {
        var branch = await _branchService.GetByIdAsync(id);
        if (branch == null)
        {
            return NotFound();
        }
        return Ok(branch);
    }

    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBranch(Guid id, [FromQuery] UpdateBranchRequest updatePhotoBoothBranchRequest, [FromQuery] BranchStatus? status)
    {
        await _branchService.UpdateAsync(id, updatePhotoBoothBranchRequest, status);
        return Ok();
    }
    //[HttpPut("{branchId}/assign-manager")]
    //public async Task<ActionResult> AssignManager(Guid branchId, [FromBody] AssignManagerRequest request)
    //{
    //    await _branchService.AssignManager(branchId, request);
    //    return Ok();
    //}
    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBranch(Guid id)
    {
        await _branchService.DeleteAsync(id);
        return Ok();
    }
}

