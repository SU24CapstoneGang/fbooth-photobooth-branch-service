using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.Services.MoMoServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PaymentMomoController : ControllerBaseApi
    {
        private readonly IMoMoService _moService;

        public PaymentMomoController(IMoMoService moService)
        {
            _moService = moService;
        }

        [HttpPost("ipn")]
        public async Task PaymentMomoIPN(MoMoResponse moMoResponse)
        {
            await _moService.HandlePaymentResponeIPN(moMoResponse);
        }

        [HttpGet("return")]
        public async Task<IActionResult> PaymentMomoReturn()
        {
            if (Request.QueryString.HasValue)
            {
                await _moService.Return(Request.Query);
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
                    </body>
                    </html>";

                return Content(returnContent, "text/html");
            }
            else
            {
                return BadRequest(new { Message = "No query string found" });
            }
        }
    }
}
