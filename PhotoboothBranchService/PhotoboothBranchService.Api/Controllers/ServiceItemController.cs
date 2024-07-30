using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Application.Services.ServiceItemServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [Route("api/service-item")]
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

            var createServiceItemResponse = await _serviceItemService.CreateAsync(createServiceItemRequest);
            return Ok(createServiceItemResponse);

        }

        //add list
        [HttpPost("add-list")]
        public async Task<ActionResult<AddListServiceItemResponse>> AddListItems(AddListServiceItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _serviceItemService.AddListServiceItem(request);
            return Ok(response);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceItemResponse>>> GetAllServiceItems()
        {

            var serviceItems = await _serviceItemService.GetAllAsync();
            return Ok(serviceItems);

        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<ServiceItemResponse>>> GetAllServiceItems(
            [FromQuery] ServiceItemFilter serviceItemFilter, [FromQuery] PagingModel pagingModel)
        {
            var serviceItems = await _serviceItemService.GetAllPagingAsync(serviceItemFilter, pagingModel);
            return Ok(serviceItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceItemResponse>> GetServiceItemById(Guid id)
        {
            var serviceItem = await _serviceItemService.GetByIdAsync(id);
            if (serviceItem == null)
            {
                return NotFound();
            }
            return Ok(serviceItem);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateServiceItem(Guid id, [FromQuery] UpdateServiceItemRequest updateServiceItemRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _serviceItemService.UpdateAsync(id, updateServiceItemRequest);
            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServiceItem(Guid id)
        {
            await _serviceItemService.DeleteAsync(id);
            return Ok();
        }
    }
}
