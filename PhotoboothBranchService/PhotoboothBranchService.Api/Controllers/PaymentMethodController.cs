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
        public async Task<ActionResult<CreatePaymentMethodResponse>> CreatePaymentMethod(CreatePaymentMethodRequest createPaymentMethodRequest)
        {

            var createPaymentMethodResponse = await _paymentMethodService.CreateAsync(createPaymentMethodRequest);
            return Ok(createPaymentMethodResponse);

        }

        // Read all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentMethodResponse>>> GetAllPaymentMethods()
        {

            var paymentMethods = await _paymentMethodService.GetAllAsync();
            return Ok(paymentMethods);

        }

        // Read all with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PaymentMethodResponse>>> GetPagingPaymentMethods(
            [FromQuery] PaymentMethodFilter paymentMethodFilter, [FromQuery] PagingModel pagingModel)
        {

            var paymentMethods = await _paymentMethodService.GetAllPagingAsync(paymentMethodFilter, pagingModel);
            return Ok(paymentMethods);

        }

        // Read by name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<IEnumerable<PaymentMethodResponse>>> GetPaymentMethodsByName(string name)
        {

            var paymentMethods = await _paymentMethodService.GetByName(name);
            return Ok(paymentMethods);

        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodResponse>> GetPaymentMethodById(Guid id)
        {

            var paymentMethod = await _paymentMethodService.GetByIdAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return Ok(paymentMethod);

        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePaymentMethod(Guid id, UpdatePaymentMethodRequest updatePaymentMethodRequest)
        {

            await _paymentMethodService.UpdateAsync(id, updatePaymentMethodRequest);
            return Ok();

        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentMethod(Guid id)
        {

            await _paymentMethodService.DeleteAsync(id);
            return Ok();

        }
    }
}
