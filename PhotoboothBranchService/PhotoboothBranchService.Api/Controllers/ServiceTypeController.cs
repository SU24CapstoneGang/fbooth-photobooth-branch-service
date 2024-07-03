using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Application.Services.ServiceTypeServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class ServiceTypeController : ControllerBaseApi
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreateServiceTypeResponse>> CreateServiceType(CreateServiceTypeRequest createServiceTypeRequest)
        {
            try
            {
                var createServiceTypeResponse = await _serviceTypeService.CreateAsync(createServiceTypeRequest);
                return Ok(createServiceTypeResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the service type: {ex.Message}");
            }
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponse>>> GetAllServiceTypes()
        {
            try
            {
                var serviceTypes = await _serviceTypeService.GetAllAsync();
                return Ok(serviceTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving service types: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponse>>> GetAllServiceTypes(
            [FromQuery] ServiceTypeFilter serviceTypeFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var serviceTypes = await _serviceTypeService.GetAllPagingAsync(serviceTypeFilter, pagingModel);
                return Ok(serviceTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving service types: {ex.Message}");
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponse>>> GetServiceTypesByName(string name)
        {
            try
            {
                var serviceTypes = await _serviceTypeService.GetByName(name);
                return Ok(serviceTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving service types by name: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceTypeResponse>> GetServiceTypeById(Guid id)
        {
            try
            {
                var serviceType = await _serviceTypeService.GetByIdAsync(id);
                if (serviceType == null)
                {
                    return NotFound();
                }
                return Ok(serviceType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the service type by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateServiceType(Guid id, UpdateServiceTypeRequest updateServiceTypeRequest)
        {
            try
            {
                await _serviceTypeService.UpdateAsync(id, updateServiceTypeRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the service type: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServiceType(Guid id)
        {
            try
            {
                await _serviceTypeService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the service type: {ex.Message}");
            }
        }
    }
}
