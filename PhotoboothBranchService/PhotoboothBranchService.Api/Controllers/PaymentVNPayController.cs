using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;
using PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PaymentVNPayController : ControllerBaseApi
    {
        private readonly IVNPayService _vnpayService;

        public PaymentVNPayController(IVNPayService vnpayService)
        {
            _vnpayService = vnpayService;
        }

        [HttpPost]
        public async Task<string> CreatePaymentRequest([FromBody] VnpayRequest paymentRequest)
        {
            try
            {
                var clientIpAddress = IpAddressHelper.GetClientIpAddress(HttpContext);
                paymentRequest.ClientIpAddress = clientIpAddress;
                string paymentUrl = await _vnpayService.Pay(paymentRequest);
                return paymentUrl;
                //string qrcode = await _qrCodeService.GetQrCodeDataAsync(paymentUrl);
                //return qrcode;
            }
            catch (Exception ex)
            {
                //return StatusCode(500, $"An error occurred while creating the request: {ex.Message}");
                return ex.Message;
            }
        }

        [HttpPost("query")]
        public async Task<IActionResult> QueryTransaction([FromBody] VnpayQueryRequest request)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            var result = await _vnpayService.Query(request.SessionId, request.PayDate, clientIp);
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
        public IActionResult VnpayReturn()
        {
            if (Request.QueryString.HasValue)
            {
                var response = _vnpayService.Return(Request.Query);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }

            return BadRequest(new { Message = "No query string found" });
        }
    }
}
