using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;
using PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PaymentController : ControllerBaseApi
    {
        private readonly IVNPayService _vNPayService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentController(IVNPayService vNPayService, IHttpContextAccessor httpContextAccessor)
        {
            _vNPayService = vNPayService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePaymentRequest([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var clientIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
                paymentRequest.ClientIpAddress = clientIpAddress;
                string paymentUrl =  await _vNPayService.Pay(paymentRequest);
                return Redirect(paymentUrl);
            } catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the request: {ex.Message}");
            }
        }
    }
}
