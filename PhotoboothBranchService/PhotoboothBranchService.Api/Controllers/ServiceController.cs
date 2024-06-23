using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.Services.ServiceServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class ServiceController : ControllerBaseApi
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreateServiceResponse>> CreateService(CreateServiceRequest createServiceRequest)
        {
            try
            {
                var createServiceResponse = await _serviceService.CreateAsync(createServiceRequest);
                return Ok(createServiceResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the service: {ex.Message}");
            }
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAllServices()
        {
            try
            {
                var services = await _serviceService.GetAllAsync();
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving services: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAllServices(
            [FromQuery] ServiceFilter serviceFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var services = await _serviceService.GetAllPagingAsync(serviceFilter, pagingModel);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving services: {ex.Message}");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetServicesByName(string name)
        {
            try
            {
                var services = await _serviceService.GetByName(name);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving services by name: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetServiceById(Guid id)
        {
            try
            {
                var service = await _serviceService.GetByIdAsync(id);
                if (service == null)
                {
                    return NotFound();
                }
                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the service by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateService(Guid id, UpdateServiceRequest updateServiceRequest)
        {
            try
            {
                await _serviceService.UpdateAsync(id, updateServiceRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the service: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(Guid id)
        {
            try
            {
                await _serviceService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the service: {ex.Message}");
            }
        }
    }
}
