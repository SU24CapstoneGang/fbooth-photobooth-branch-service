using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.DTOs.VNPayPayment;
using PhotoboothBranchService.Application.Services.VNPayServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PaymentVNPayController : ControllerBaseApi
    {
        private readonly IVNPayService _vnpayService;
        public PaymentVNPayController(IVNPayService vnpayService)
        {
            _vnpayService = vnpayService;
        }

        [HttpPost("query")]
        public async Task<IActionResult> QueryTransaction([FromBody] VnpayQueryRequest request)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            var result = await _vnpayService.Query(request.PaymentID, request.PayDate, clientIp);
            return Ok(result);
        }

        [HttpPost("refund")]
        public async Task<IActionResult> RefundTransaction([FromBody] VnpayRefundRequest request)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            var result = await _vnpayService.RefundTransaction(request, clientIp);
            return Ok(result);
        }

        [HttpGet("return")]
        public async Task<IActionResult> VnpayReturn()
        {
            if (Request.QueryString.HasValue)
            {
                var response = await _vnpayService.Return(Request.Query);
                string returnContent = $@"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <title>Payment Return</title>
                        <script>
                            window.onload = function() {{
                                setTimeout(function() {{
                                    window.close();
                                }}, 3000); // Close after 3 seconds
                            }}
                        </script>
                    </head>
                    <body>
                        <h1>Payment Processed</h1>
                        <p>Your payment has been processed. This tab will close automatically.</p>
                        <h2>Response Data:</h2>
                        <pre>{response.returnContent}</pre> <!-- Display JSON response here -->
                    </body>
                    </html>";

                return Content(returnContent, "text/html");
            }
            return BadRequest(new { Message = "No query string found" });
        }
    }
}
