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
        try
        {
            var createBoothBranchResponse = await _photoBoothBranchService.CreateAsync(createPhotoBoothBranchRequest);
            return Ok(createBoothBranchResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the branch: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetAllBranches()
    {
        try
        {
            var branches = await _photoBoothBranchService.GetAllAsync();
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches: {ex.Message}");
        }
    }
    //get all with filter and paging
    [HttpGet("paging")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetAllBranches(
        [FromQuery] BranchFilter photoBoothBranchFilter, [FromQuery] PagingModel pagingModel)
    {
        try
        {
            var branches = await _photoBoothBranchService.GetAllPagingAsync(photoBoothBranchFilter, pagingModel);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches: {ex.Message}");
        }
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetBranchesByStatus(ManufactureStatus status)
    {
        try
        {
            var branches = await _photoBoothBranchService.GetByStatus(status);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches by status: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetBranchesByName(string name)
    {
        try
        {
            var branches = await _photoBoothBranchService.SearchByName(name);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BranchResponse>> GetBranchById(Guid id)
    {
        try
        {
            var branch = await _photoBoothBranchService.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return Ok(branch);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the branch by ID: {ex.Message}");
        }
    }

    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBranch(Guid id, UpdateBranchRequest updatePhotoBoothBranchRequest)
    {
        try
        {
            await _photoBoothBranchService.UpdateAsync(id, updatePhotoBoothBranchRequest);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the branch: {ex.Message}");
        }
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBranch(Guid id)
    {
        try
        {
            await _photoBoothBranchService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the branch: {ex.Message}");
        }
    }
}

