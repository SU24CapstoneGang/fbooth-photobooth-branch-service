// PhotoBoothBranchesController.cs
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PhotoBoothBranchesController : ControllerBaseApi
    {
        private readonly IPhotoBoothBranchesRepository _photoBoothBranchesRepository;

        public PhotoBoothBranchesController(IPhotoBoothBranchesRepository photoBoothBranchesRepository)
        {
            _photoBoothBranchesRepository = photoBoothBranchesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoBoothBranches>>> GetAllPhotoBoothBranches(CancellationToken cancellationToken)
        {
            try
            {
                var branches = await _photoBoothBranchesRepository.GetAll(cancellationToken);
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving branches: {ex.Message}");
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<PhotoBoothBranches>>> GetPhotoBoothBranchesByStatus(ManufactureStatus status, CancellationToken cancellationToken)
        {
            try
            {
                var branches = await _photoBoothBranchesRepository.GetAll(status, cancellationToken);
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving branches by status: {ex.Message}");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<PhotoBoothBranches>>> GetPhotoBoothBranchesByName(string name, CancellationToken cancellationToken)
        {
            try
            {
                var branches = await _photoBoothBranchesRepository.GetByName(name, cancellationToken);
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving branches by name: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePhotoBoothBranch(PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken)
        {
            try
            {
                await _photoBoothBranchesRepository.AddAsync(photoBoothBranch, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the branch: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoBoothBranches>> GetPhotoBoothBranchById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await _photoBoothBranchesRepository.GetByIdAsync(id, cancellationToken);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhotoBoothBranch(Guid id, PhotoBoothBranches photoBoothBranch, CancellationToken cancellationToken)
        {
            try
            {
                if (id != photoBoothBranch.Id)
                {
                    return BadRequest("Invalid ID.");
                }

                await _photoBoothBranchesRepository.UpdateAsync(photoBoothBranch, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the branch: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhotoBoothBranch(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await _photoBoothBranchesRepository.GetByIdAsync(id, cancellationToken);
                if (branch == null)
                {
                    return NotFound();
                }

                await _photoBoothBranchesRepository.RemoveAsync(branch, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the branch: {ex.Message}");
            }
        }
    }
}
