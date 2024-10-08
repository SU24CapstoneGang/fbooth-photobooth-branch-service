﻿// PhotoBoothBranchesController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Application.DTOs.Layout;
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
    public async Task<ActionResult<CreateBranchResponse>> CreateBranch(CreateBranchRequest createPhotoBoothBranchRequest)
    {
        var createBoothBranchResponse = await _branchService.CreateAsync(createPhotoBoothBranchRequest);
        return Ok(createBoothBranchResponse);
    }

    [HttpPost("{branchID}/photos")]
    public async Task<IActionResult> AddPhotoForBranch(Guid branchID, IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file provided.");
        }

        try
        {
            var response = await _branchService.AddPhotoForBranch(branchID, file);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
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

    [HttpGet("customer")]
    public async Task<ActionResult<IEnumerable<BranchResponse>>> GetAvailbleStickerTypes()
    {
        var branches = await _branchService.GetAvailbleAsync();
        return Ok(branches);
    }
    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBranch(Guid id, [FromBody] UpdateBranchRequest updatePhotoBoothBranchRequest)
    {
        await _branchService.UpdateAsync(id, updatePhotoBoothBranchRequest);
        return Ok();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBranch(Guid id)
    {
        await _branchService.DeleteAsync(id);
        return Ok();
    }
}

