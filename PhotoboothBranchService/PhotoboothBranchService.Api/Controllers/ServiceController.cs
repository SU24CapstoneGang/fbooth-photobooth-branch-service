using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
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

            var createServiceResponse = await _serviceService.CreateAsync(createServiceRequest);
            return Ok(createServiceResponse);

        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAllServices()
        {

            var services = await _serviceService.GetAllAsync();
            return Ok(services);

        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAllServices(
            [FromQuery] ServiceFilter serviceFilter, [FromQuery] PagingModel pagingModel)
        {

            var services = await _serviceService.GetAllPagingAsync(serviceFilter, pagingModel);
            return Ok(services);

        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetServicesByName(string name)
        {

            var services = await _serviceService.GetByName(name);
            return Ok(services);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse>> GetServiceById(Guid id)
        {

            var service = await _serviceService.GetByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);

        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateService(Guid id, UpdateServiceRequest updateServiceRequest)
        {

            await _serviceService.UpdateAsync(id, updateServiceRequest);
            return Ok();

        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService(Guid id)
        {

            await _serviceService.DeleteAsync(id);
            return Ok();

        }
    }
}
