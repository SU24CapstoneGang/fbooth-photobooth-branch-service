using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Application.Services.PaymentMethodServices;

namespace PhotoboothBranchService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePaymentMethod(CreatePaymentMethodRequest createPaymentMethodRequest)
        {
            try
            {
                var id = await _paymentMethodService.CreateAsync(createPaymentMethodRequest);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the paymentMethod: {ex.Message}");
            }
        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethodResponse>>> GetAllPaymentMethods()
        {
            try
            {
                var paymentMethods = await _paymentMethodService.GetAllAsync();
                return Ok(paymentMethods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving paymentMethods: {ex.Message}");
            }
        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PaymentMethodResponse>>> GetPagingPaymentMethods(
            [FromQuery] PaymentMethodFilter paymentMethodFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var paymentMethods = await _paymentMethodService.GetAllPagingAsync(paymentMethodFilter, pagingModel);
                return Ok(paymentMethods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving paymentMethods: {ex.Message}");
            }
        }

        // Read by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<PaymentMethodResponse>>> GetPaymentMethodsByName(string name)
        {
            try
            {
                var paymentMethods = await _paymentMethodService.GetByName(name);
                return Ok(paymentMethods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving paymentMethods by name: {ex.Message}");
            }
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodResponse>> GetPaymentMethodById(Guid id)
        {
            try
            {
                var paymentMethod = await _paymentMethodService.GetByIdAsync(id);
                if (paymentMethod == null)
                {
                    return NotFound();
                }
                return Ok(paymentMethod);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the paymentMethod by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaymentMethod(Guid id, UpdatePaymentMethodRequest updatePaymentMethodRequest)
        {
            try
            {
                await _paymentMethodService.UpdateAsync(id, updatePaymentMethodRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the paymentMethod: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentMethod(Guid id)
        {
            try
            {
                await _paymentMethodService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the paymentMethod: {ex.Message}");
            }
        }
    }
}
