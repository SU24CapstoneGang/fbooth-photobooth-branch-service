﻿using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.Services.ServiceServices;
using PhotoboothBranchService.Domain.Enum;

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
        public async Task<ActionResult<CreateServiceResponse>> CreateService([FromBody]CreateServiceRequest createServiceRequest, StatusUse status)
        {

            var createServiceResponse = await _serviceService.CreateAsync(createServiceRequest, status);
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
        public async Task<ActionResult> UpdateService(Guid id, [FromQuery] UpdateServiceRequest updateServiceRequest, [FromQuery]StatusUse? status)
        {

            await _serviceService.UpdateAsync(id, updateServiceRequest, status);
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
