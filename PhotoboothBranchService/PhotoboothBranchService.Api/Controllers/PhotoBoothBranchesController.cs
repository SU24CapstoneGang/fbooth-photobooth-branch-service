using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers;

// PhotoBoothBranchesController.cs
[ApiController]
[Route("api/[controller]")]
public class PhotoBoothBranchesController : ControllerBase
{
    private readonly IPhotoBoothBranchesRepository _photoBoothBranchesRepository;

    public PhotoBoothBranchesController(IPhotoBoothBranchesRepository photoBoothBranchesRepository)
    {
        _photoBoothBranchesRepository = photoBoothBranchesRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranches>>> GetAllPhotoBoothBranches(CancellationToken cancellationToken)
    {
        var branches = await _photoBoothBranchesRepository.GetAll(cancellationToken);
        return Ok(branches);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranches>>> GetPhotoBoothBranchesByStatus(ManufactureStatus status, CancellationToken cancellationToken)
    {
        var branches = await _photoBoothBranchesRepository.GetAll(status, cancellationToken);
        return Ok(branches);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<IEnumerable<PhotoBoothBranches>>> GetPhotoBoothBranchesByName(string name, CancellationToken cancellationToken)
    {
        var branches = await _photoBoothBranchesRepository.GetByName(name, cancellationToken);
        return Ok(branches);
    }

    [HttpPost]
    public async Task<ActionResult> CreatePhotoBoothBranch(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken)
    {
        await _photoBoothBranchesRepository.AddAsync(photoBoothBranch, cancellationToken);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PhotoBoothBranches>> GetPhotoBoothBranchById(Guid id, CancellationToken cancellationToken)
    {
        var branch = await _photoBoothBranchesRepository.GetByIdAsync(id, cancellationToken);
        if (branch == null)
        {
            return NotFound();
        }
        return Ok(branch);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePhotoBoothBranch(Guid id, PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken)
    {
        if (id != photoBoothBranch.Id)
        {
            return BadRequest("Invalid ID.");
        }

        await _photoBoothBranchesRepository.UpdateAsync(photoBoothBranch, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePhotoBoothBranch(Guid id, CancellationToken cancellationToken)
    {
        var branch = await _photoBoothBranchesRepository.GetByIdAsync(id, cancellationToken);
        if (branch == null)
        {
            return NotFound();
        }

        await _photoBoothBranchesRepository.RemoveAsync(branch, cancellationToken);
        return NoContent();
    }
}

