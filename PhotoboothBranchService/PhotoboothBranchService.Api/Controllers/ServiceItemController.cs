using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Application.Services.ServiceItemServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class ServiceItemController : ControllerBaseApi
    {
        private readonly IServiceItemService _serviceItemService;

        public ServiceItemController(IServiceItemService serviceItemService)
        {
            _serviceItemService = serviceItemService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreateServiceItemResponse>> CreateServiceItem(CreateServiceItemRequest createServiceItemRequest)
        {
            try
            {
                var createServiceItemResponse = await _serviceItemService.CreateAsync(createServiceItemRequest);
                return Ok(createServiceItemResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the service item: {ex.Message}");
            }
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceItemResponse>>> GetAllServiceItems()
        {
            try
            {
                var serviceItems = await _serviceItemService.GetAllAsync();
                return Ok(serviceItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving service items: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ServiceItemResponse>>> GetAllServiceItems(
            [FromQuery] ServiceItemFilter serviceItemFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var serviceItems = await _serviceItemService.GetAllPagingAsync(serviceItemFilter, pagingModel);
                return Ok(serviceItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving service items: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceItemResponse>> GetServiceItemById(Guid id)
        {
            try
            {
                var serviceItem = await _serviceItemService.GetByIdAsync(id);
                if (serviceItem == null)
                {
                    return NotFound();
                }
                return Ok(serviceItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the service item by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateServiceItem(Guid id, UpdateServiceItemRequest updateServiceItemRequest)
        {
            try
            {
                await _serviceItemService.UpdateAsync(id, updateServiceItemRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the service item: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServiceItem(Guid id)
        {
            try
            {
                await _serviceItemService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the service item: {ex.Message}");
            }
        }
    }
}
