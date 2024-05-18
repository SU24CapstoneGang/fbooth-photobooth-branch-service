// PhotoBoothBranchesController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.Services.PhotoBoothBranchServices;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
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
    public async Task<ActionResult<Guid>> CreatePhotoBoothBranch(PhotoBoothBranchDTO photoBoothBranchDTO)
    {
        try
        {
            var id = await _photoBoothBranchService.CreateAsync(photoBoothBranchDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the branch: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranchDTO>>> GetAllPhotoBoothBranches()
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

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranchDTO>>> GetPhotoBoothBranchesByStatus(ManufactureStatus status)
    {
        try
        {
            var branches = await _photoBoothBranchService.GetAll(status);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches by status: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranchDTO>>> GetPhotoBoothBranchesByName(string name)
    {
        try
        {
            var branches = await _photoBoothBranchService.GetByName(name);
            return Ok(branches);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving branches by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoBoothBranchDTO>> GetPhotoBoothBranchById(Guid id)
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
    public async Task<ActionResult> UpdatePhotoBoothBranch(Guid id, PhotoBoothBranchDTO photoBoothBranchDTO)
    {
        try
        {
            await _photoBoothBranchService.UpdateAsync(id, photoBoothBranchDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the branch: {ex.Message}");
        }
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePhotoBoothBranch(Guid id)
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

