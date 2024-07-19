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
            var createServiceTypeResponse = await _serviceTypeService.CreateAsync(createServiceTypeRequest);
            return Ok(createServiceTypeResponse);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponse>>> GetAllServiceTypes()
        {
            var serviceTypes = await _serviceTypeService.GetAllAsync();
            return Ok(serviceTypes);
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponse>>> GetAllServiceTypes(
            [FromQuery] ServiceTypeFilter serviceTypeFilter, [FromQuery] PagingModel pagingModel)
        {
            var serviceTypes = await _serviceTypeService.GetAllPagingAsync(serviceTypeFilter, pagingModel);
            return Ok(serviceTypes);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ServiceTypeResponse>>> GetServiceTypesByName(string name)
        {
            var serviceTypes = await _serviceTypeService.GetByName(name);
            return Ok(serviceTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceTypeResponse>> GetServiceTypeById(Guid id)
        {
            var serviceType = await _serviceTypeService.GetByIdAsync(id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return Ok(serviceType);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateServiceType(Guid id, UpdateServiceTypeRequest updateServiceTypeRequest)
        {
            await _serviceTypeService.UpdateAsync(id, updateServiceTypeRequest);
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServiceType(Guid id)
        {
            await _serviceTypeService.DeleteAsync(id);
            return Ok();
        }
    }
}
