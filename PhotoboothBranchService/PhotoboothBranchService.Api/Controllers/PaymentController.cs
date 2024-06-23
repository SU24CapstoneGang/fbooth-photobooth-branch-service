using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Application.Services.PaymentServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PaymentController : ControllerBaseApi
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Create
        [HttpPost]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment(CreatePaymentRequest createPaymentRequest)
        {
            try
            {
                //get ip from request
                var clientIpAddress = IpAddressHelper.GetClientIpAddress(HttpContext);
                createPaymentRequest.ClientIpAddress = clientIpAddress;

                //sent to service layer
                var createPaymentResponse = await _paymentService.CreateAsync(createPaymentRequest);

                return Ok(createPaymentResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the payment: {ex.Message}");
            }
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetAllPayments()
        {
            try
            {
                var payments = await _paymentService.GetAllAsync();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving payments: {ex.Message}");
            }
        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetAllPayments(
            [FromQuery] PaymentFilter paymentFilter, [FromQuery] PagingModel pagingModel)
        {
            try
            {
                var payments = await _paymentService.GetAllPagingAsync(paymentFilter, pagingModel);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving payments: {ex.Message}");
            }
        }

        [HttpGet("transaction/{transactionId}")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetPaymentsByTransactionId(Guid transactionId)
        {
            try
            {
                var payments = await _paymentService.GetByIdAsync(transactionId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving payments by transaction ID: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentResponse>> GetPaymentById(Guid id)
        {
            try
            {
                var payment = await _paymentService.GetByIdAsync(id);
                if (payment == null)
                {
                    return NotFound();
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the payment by ID: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePayment(Guid id, UpdatePaymentRequest updatePaymentRequest)
        {
            try
            {
                await _paymentService.UpdateAsync(id, updatePaymentRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the payment: {ex.Message}");
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(Guid id)
        {
            try
            {
                await _paymentService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the payment: {ex.Message}");
            }
        }
    }
}
