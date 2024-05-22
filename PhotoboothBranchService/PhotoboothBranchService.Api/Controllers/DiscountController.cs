using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Discount;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Discount;
using PhotoboothBranchService.Application.Services.DiscountServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class DiscountController : ControllerBaseApi
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateDiscount(CreateDiscountRequest createDiscountRequest)
        {
            try
            {
                var id = await _discountService.CreateAsync(createDiscountRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the discount: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discountresponse>>> GetAllDiscounts()
        {
            try
            {
                var discounts = await _discountService.GetAllAsync();
                return Ok(discounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving discounts: {ex.Message}");
            }
        }
        // Read all with filter and paging
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<Discountresponse>>> GetPagingDiscounts(
            [FromQuery] DiscountFilter discountFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var discounts = await _discountService.GetAllPagingAsync(discountFilter, pagingModel);
                return Ok(discounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving discounts: {ex.Message}");
            }
        }
        // Read by code
        [HttpGet("code/{code}")]
        public async Task<ActionResult<IEnumerable<Discountresponse>>> GetDiscountsByCode(string code)
        {
            try
            {
                var discounts = await _discountService.SearchByCode(code);
                return Ok(discounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving discounts by code: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Discountresponse>> GetDiscountById(Guid id)
        {
            try
            {
                var discount = await _discountService.GetByIdAsync(id);
                if (discount == null)
                {
                    return NotFound();
                }
                return Ok(discount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the discount by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDiscount(Guid id, UpdateDiscountRequest updateDiscountRequest)
        {
            try
            {
                await _discountService.UpdateAsync(id, updateDiscountRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the discount: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDiscount(Guid id)
        {
            try
            {
                await _discountService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the discount: {ex.Message}");
            }
        }
    }
}
