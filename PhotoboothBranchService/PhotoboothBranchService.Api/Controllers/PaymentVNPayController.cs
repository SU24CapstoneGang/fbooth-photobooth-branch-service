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

    }
}
