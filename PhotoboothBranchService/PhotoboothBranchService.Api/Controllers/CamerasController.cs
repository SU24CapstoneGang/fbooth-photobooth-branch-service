using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Api.Controllers;

// CamerasController.cs
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
        var cameras = await _camerasRepository.GetAll(cancellationToken);
        return Ok(cameras);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<Cameras>>> GetCamerasByName(string name, CancellationToken cancellationToken)
    {
        var cameras = await _camerasRepository.GetByName(name, cancellationToken);
        return Ok(cameras);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCamera(Cameras camera, CancellationToken cancellationToken)
    {
        await _camerasRepository.AddAsync(camera, cancellationToken);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cameras>> GetCameraById(Guid id, CancellationToken cancellationToken)
    {
        var camera = await _camerasRepository.GetByIdAsync(id, cancellationToken);
        if (camera == null)
        {
            return NotFound();
        }
        return Ok(camera);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCamera(Guid id, Cameras camera, CancellationToken cancellationToken)
    {
        if (id != camera.Id)
        {
            return BadRequest("Invalid ID.");
        }

        await _camerasRepository.UpdateAsync(camera, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCamera(Guid id, CancellationToken cancellationToken)
    {
        var camera = await _camerasRepository.GetByIdAsync(id, cancellationToken);
        if (camera == null)
        {
            return NotFound();
        }

        await _camerasRepository.RemoveAsync(camera, cancellationToken);
        return NoContent();
    }
}

