using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Application.Services.MoMoServices;
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreatePaymentResponse>> CreatePayment(CreatePaymentRequest createPaymentRequest)
        {
            //get ip from request
            var clientIpAddress = IpAddressHelper.GetClientIpAddress(HttpContext);
            var email = Request.HttpContext.Items["Email"]?.ToString();
            //sent to service layer
            var createPaymentResponse = await _paymentService.CreateAsync(createPaymentRequest, clientIpAddress, email);
            return Ok(createPaymentResponse);
        }

        // Read
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetAllPayments()
        {

            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);

        }

        // Read with paging and filter
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetAllPayments(
            [FromQuery] PaymentFilter paymentFilter, [FromQuery] PagingModel pagingModel)
        {

            var payments = await _paymentService.GetAllPagingAsync(paymentFilter, pagingModel);
            return Ok(payments);

        }

        [HttpGet("transaction/{transactionId}")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetPaymentsByTransactionId(Guid transactionId)
        {
            var payments = await _paymentService.GetByIdAsync(transactionId);
            return Ok(payments);

        }
        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetPaymentsByBookingId(Guid bookingId)
        {
            var payments = await _paymentService.GetByBookingAsync(bookingId);
            return Ok(payments);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetPaymentById(Guid id)
        {

            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);

        }

        [HttpGet("customer")]
        [Authorization("CUSTOMER")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetCustomerPayment()
        {
            var email = Request.HttpContext.Items["Email"]?.ToString();
            return Ok(await _paymentService.GetCustomerTransaction(email));
        }
        [Authorization("STAFF")]
        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> StaffGetBranchTransaction() 
        {
            var email = Request.HttpContext.Items["Email"]?.ToString();
            return Ok(await _paymentService.StaffGetBranchTransaction(email));
        }
        [Authorization("ADMIN")]
        [HttpGet("admin/{BranchID}")]
        public async Task<ActionResult<IEnumerable<PaymentResponse>>> GetBranchTransaction(Guid BranchID)
        {
            return Ok(await _paymentService.GetBranchTransaction(BranchID));
        }
        // Update
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePayment(Guid id, UpdatePaymentRequest updatePaymentRequest)
        {

            await _paymentService.UpdateAsync(id, updatePaymentRequest);
            return Ok();

        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayment(Guid id)
        {

            await _paymentService.DeleteAsync(id);
            return Ok();

        }

        ////handle return 
        //[HttpGet("vnpay/return")]
        //public async Task<IActionResult> VnpayReturn()
        //{
        //    if (Request.QueryString.HasValue)
        //    {
        //        var response = await _paymentService.HandleVnpayResponse(Request.Query);
        //        string returnContent = $@"
        //            <!DOCTYPE html>
        //            <html>
        //            <head>
        //                <title>Payment Return</title>
        //                <script>
        //                    window.onload = function() {{
        //                        setTimeout(function() {{
        //                            window.close();
        //                        }}, 3000); // Close after 3 seconds
        //                    }}
        //                </script>
        //            </head>
        //            <body>
        //                <h1>Payment Processed</h1>
        //                <p>Your payment has been processed. This tab will close automatically.</p>
        //                <h2>Response Data:</h2>
        //                <pre>{response.returnContent}</pre> <!-- Display JSON response here -->
        //            </body>
        //            </html>";

        //        return Content(returnContent, "text/html");
        //    }
        //    return BadRequest(new { Message = "No query string found" });
        //}

        //[HttpGet("momo/return")]
        //public async Task<IActionResult> PaymentMomoReturn()
        //{
        //    if (Request.QueryString.HasValue)
        //    {
        //        await _paymentService.HandleMomoResponse(Request.Query);
        //        string returnContent = $@"
        //            <!DOCTYPE html>
        //            <html>
        //            <head>
        //                <title>Payment Return</title>
        //                <script>
        //                    window.onload = function() {{
        //                        setTimeout(function() {{
        //                            window.close();
        //                        }}, 3000); // Close after 3 seconds
        //                    }}
        //                </script>
        //            </head>
        //            <body>
        //                <h1>Payment Processed</h1>
        //                <p>Your payment has been processed. This tab will close automatically.</p>
        //            </body>
        //            </html>";

        //        return Content(returnContent, "text/html");
        //    }
        //    else
        //    {
        //        return BadRequest(new { Message = "No query string found" });
        //    }
        //}

        [HttpGet("vnpay/ipn")]
        public async Task<IActionResult> HandleVnpayIPN()
        {
            if (Request.QueryString.HasValue)
            {
                var response = await _paymentService.HandleVnpayResponse(Request.Query);
                return Ok(response.returnContent);
            }
            return BadRequest(new { Message = "No query string found" });
        }
        [HttpPost("momo/ipn")]
        public async Task<ActionResult<MomoIPNResponse>> HandleMomoIPN(MoMoResponse moMoResponse)
        {
            return Ok(await _paymentService.HandleMomoIPN(moMoResponse));
        }
    }
}
