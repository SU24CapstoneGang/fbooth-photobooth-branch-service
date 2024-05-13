// PhotoBoothBranchesController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers;

public class PhotoBoothBranchesController : ControllerBaseApi
{
    private readonly IPhotoBoothBranchService _photoBoothBranchService;

    public PhotoBoothBranchesController(IPhotoBoothBranchService photoBoothBranchService)
    {
        _photoBoothBranchService = photoBoothBranchService;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreatePhotoBoothBranch(PhotoBoothBranchDTO photoBoothBranchDTO, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _photoBoothBranchService.CreateAsync(photoBoothBranchDTO, cancellationToken);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the branch: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranchDTO>>> GetAllPhotoBoothBranches(CancellationToken cancellationToken)
    {
        try
        {
            var branches = await _photoBoothBranchService.GetAllAsync(cancellationToken);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches: {ex.Message}");
        }
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranchDTO>>> GetPhotoBoothBranchesByStatus(ManufactureStatus status, CancellationToken cancellationToken)
    {
        try
        {
            var branches = await _photoBoothBranchService.GetAll(status, cancellationToken);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches by status: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranchDTO>>> GetPhotoBoothBranchesByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var branches = await _photoBoothBranchService.GetByName(name, cancellationToken);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoBoothBranchDTO>> GetPhotoBoothBranchById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var branch = await _photoBoothBranchService.GetByIdAsync(id, cancellationToken);
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
    public async Task<ActionResult> UpdatePhotoBoothBranch(Guid id, PhotoBoothBranchDTO photoBoothBranchDTO, CancellationToken cancellationToken)
    {
        try
        {
            await _photoBoothBranchService.UpdateAsync(id, photoBoothBranchDTO, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the branch: {ex.Message}");
        }
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePhotoBoothBranch(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _photoBoothBranchService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the branch: {ex.Message}");
        }
    }
}

