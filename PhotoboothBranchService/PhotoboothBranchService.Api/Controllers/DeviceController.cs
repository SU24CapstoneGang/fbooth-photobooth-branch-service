using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Device;
using PhotoboothBranchService.Application.Services.DeviceServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class DeviceController : ControllerBaseApi
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreateDeviceResponse>> CreateDevice(CreateDeviceRequest createDeviceRequest)
        {
            try
            {
                var createDeviceResponse = await _deviceService.CreateAsync(createDeviceRequest);
                return Ok(createDeviceResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the device: {ex.Message}");
            }
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceResponse>>> GetAllDevices()
        {
            try
            {
                var devices = await _deviceService.GetAllAsync();
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving devices: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<DeviceResponse>>> GetAllDevices(
            [FromQuery] DeviceFilter deviceFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var devices = await _deviceService.GetAllPagingAsync(deviceFilter, pagingModel);
                return Ok(devices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving devices: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceResponse>> GetDeviceById(Guid id)
        {
            try
            {
                var device = await _deviceService.GetByIdAsync(id);
                if (device == null)
                {
                    return NotFound();
                }
                return Ok(device);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the device by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDevice(Guid id, UpdateDeviceRequest updateDeviceRequest)
        {
            try
            {
                await _deviceService.UpdateAsync(id, updateDeviceRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the device: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(Guid id)
        {
            try
            {
                await _deviceService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the device: {ex.Message}");
            }
        }
    }
}
