// CamerasController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTO;
using PhotoboothBranchService.Application.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers;

public class CamerasController : ControllerBaseApi
{
    private readonly ICameraService _cameraService;

    public CamerasController(ICameraService cameraService)
    {
        _cameraService = cameraService;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCamera(CameraDTO cameraDTO, CancellationToken cancellationToken)
    {
        try
        {
            var id = await _cameraService.CreateAsync(cameraDTO, cancellationToken);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the camera: {ex.Message}");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CameraDTO>>> GetAllCameras(CancellationToken cancellationToken)
    {
        try
        {
            var cameras = await _cameraService.GetAllAsync(cancellationToken);
            return Ok(cameras);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving cameras: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<CameraDTO>>> GetCamerasByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var cameras = await _cameraService.GetByName(name, cancellationToken);
            return Ok(cameras);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving cameras by name: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CameraDTO>> GetCameraById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var camera = await _cameraService.GetByIdAsync(id, cancellationToken);
            if (camera == null)
            {
                return NotFound();
            }
            return Ok(camera);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving the camera by ID: {ex.Message}");
        }
    }

    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCamera(Guid id, CameraDTO cameraDTO, CancellationToken cancellationToken)
    {
        try
        {
            await _cameraService.UpdateAsync(id, cameraDTO, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the camera: {ex.Message}");
        }
    }


    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCamera(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _cameraService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the camera: {ex.Message}");
        }
    }
}
