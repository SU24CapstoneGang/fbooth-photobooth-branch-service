using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Camera;
using PhotoboothBranchService.Application.DTOs.RequestModels.Common;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Camera;
using PhotoboothBranchService.Application.Services.CameraServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.Controllers
{
    public class CamerasController : ControllerBaseApi
    {
        private readonly ICameraService _cameraService;

        public CamerasController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCamera(CreateCameraRequest createCameraRequest)
        {
            try
            {
                var id = await _cameraService.CreateAsync(createCameraRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the camera: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cameraresponse>>> GetAllCameras()
        {
            try
            {
                var cameras = await _cameraService.GetAllAsync();
                return Ok(cameras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving cameras: {ex.Message}");
            }
        }
        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<Cameraresponse>>> GetPagingCameras(
            [FromBody] FilterPagingModel<CameraFilter> filterPagingModel)
        {
            try
            {
                var cameras = await _cameraService.GetAllPagingAsync(filterPagingModel.Filter,filterPagingModel.Paging);
                return Ok(cameras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving cameras: {ex.Message}");
            }
        }
        // Read by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<Cameraresponse>>> GetCamerasByName(string name)
        {
            try
            {
                var cameras = await _cameraService.GetByName(name);
                return Ok(cameras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving cameras by name: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cameraresponse>> GetCameraById(Guid id)
        {
            try
            {
                var camera = await _cameraService.GetByIdAsync(id);
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

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCamera(Guid id, UpdateCameraRequest updateCameraRequest)
        {
            try
            {
                await _cameraService.UpdateAsync(id, updateCameraRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the camera: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCamera(Guid id)
        {
            try
            {
                await _cameraService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the camera: {ex.Message}");
            }
        }
    }
}
