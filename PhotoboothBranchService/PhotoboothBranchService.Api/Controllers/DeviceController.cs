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

            var createDeviceResponse = await _deviceService.CreateAsync(createDeviceRequest);
            return Ok(createDeviceResponse);

        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceResponse>>> GetAllDevices()
        {

            var devices = await _deviceService.GetAllAsync();
            return Ok(devices);

        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<DeviceResponse>>> GetAllDevices(
            [FromQuery] DeviceFilter deviceFilter, [FromQuery] PagingModel pagingModel)
        {
            var devices = await _deviceService.GetAllPagingAsync(deviceFilter, pagingModel);
            return Ok(devices);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceResponse>> GetDeviceById(Guid id)
        {

            var device = await _deviceService.GetByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }
            return Ok(device);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDevice(Guid id, UpdateDeviceRequest updateDeviceRequest)
        {
            await _deviceService.UpdateAsync(id, updateDeviceRequest);
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(Guid id)
        {
            await _deviceService.DeleteAsync(id);
            return Ok();
        }
    }
}
