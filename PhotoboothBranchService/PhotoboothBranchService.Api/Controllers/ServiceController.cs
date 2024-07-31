using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.Services.ServiceServices;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Api.Controllers
{
    public class ServiceController : ControllerBaseApi
    {
        private readonly IServiceService _serviceTypeService;

        public ServiceController(IServiceService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreateServiceResponse>> CreateServiceType([FromBody]CreateServiceRequest createServiceTypeRequest, StatusUse status)
        {
            var createServiceTypeResponse = await _serviceTypeService.CreateAsync(createServiceTypeRequest, status);
            return Ok(createServiceTypeResponse);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAllServiceTypes()
        {
            var serviceTypes = await _serviceTypeService.GetAllAsync();
            return Ok(serviceTypes);
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAllServiceTypes(
            [FromQuery] ServiceFilter serviceTypeFilter, [FromQuery] PagingModel pagingModel)
        {
            var serviceTypes = await _serviceTypeService.GetAllPagingAsync(serviceTypeFilter, pagingModel);
            return Ok(serviceTypes);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetServiceTypesByName(string name)
        {
            var serviceTypes = await _serviceTypeService.GetByName(name);
            return Ok(serviceTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetServiceTypeById(Guid id)
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
        public async Task<ActionResult> UpdateServiceType(Guid id, [FromQuery] UpdateServiceRequest updateServiceTypeRequest, [FromQuery] StatusUse status)
        {
            await _serviceTypeService.UpdateAsync(id, updateServiceTypeRequest, status);
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
