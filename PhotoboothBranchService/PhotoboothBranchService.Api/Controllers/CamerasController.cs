// CamerasController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CamerasController : ControllerBase
{
    private readonly ICamerasRepository _camerasRepository;

    public CamerasController(ICamerasRepository camerasRepository)
    {
        _camerasRepository = camerasRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cameras>>> GetAllCameras(CancellationToken cancellationToken)
    {
        try
        {
            var cameras = await _camerasRepository.GetAll(cancellationToken);
            return Ok(cameras);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving cameras: {ex.Message}");
        }
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<Cameras>>> GetCamerasByName(string name, CancellationToken cancellationToken)
    {
        try
        {
            var cameras = await _camerasRepository.GetByName(name, cancellationToken);
            return Ok(cameras);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving cameras by name: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateCamera(Cameras camera, CancellationToken cancellationToken)
    {
        try
        {
            await _camerasRepository.AddAsync(camera, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the camera: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cameras>> GetCameraById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var camera = await _camerasRepository.GetByIdAsync(id, cancellationToken);
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

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCamera(Guid id, Cameras camera, CancellationToken cancellationToken)
    {
        try
        {
            if (id != camera.Id)
            {
                return BadRequest("Invalid ID.");
            }

            await _camerasRepository.UpdateAsync(camera, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the camera: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCamera(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var camera = await _camerasRepository.GetByIdAsync(id, cancellationToken);
            if (camera == null)
            {
                return NotFound();
            }

            await _camerasRepository.RemoveAsync(camera, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the camera: {ex.Message}");
        }
    }
}
