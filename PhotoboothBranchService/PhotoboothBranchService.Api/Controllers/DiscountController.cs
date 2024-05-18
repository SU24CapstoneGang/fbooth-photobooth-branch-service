using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.Services.DiscountServices;

namespace PhotoboothBranchService.Api.Controllers;

public class DiscountController : ControllerBaseApi
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    // Create
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateDiscount(DiscountDTO discountDTO)
    {
        try
        {
            var id = await _discountService.CreateAsync(discountDTO);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the discount: {ex.Message}");
        }
    }

    // Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiscountDTO>>> GetAllDiscounts()
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

    [HttpGet("code/{code}")]
    public async Task<ActionResult<IEnumerable<DiscountDTO>>> GetDiscountsByCode(string code)
    {
        try
        {
            var discounts = await _discountService.GetByCode(code);
            return Ok(discounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving discounts by code: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DiscountDTO>> GetDiscountById(Guid id)
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
    public async Task<ActionResult> UpdateDiscount(Guid id, DiscountDTO discountDTO)
    {
        try
        {
            await _discountService.UpdateAsync(id, discountDTO);
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

