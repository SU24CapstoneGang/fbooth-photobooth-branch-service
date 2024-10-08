﻿using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Api.Common.Helper;
using PhotoboothBranchService.Application.DTOs.Refund;
using PhotoboothBranchService.Application.Services.RefundServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class RefundController : ControllerBaseApi
    {
        private readonly IRefundService _refundService;

        public RefundController(IRefundService refundService)
        {
            _refundService = refundService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RefundResponse>>> GetAll()
        {
            var refunds = await _refundService.GetAllAsync();
            return Ok(refunds);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RefundResponse>> GetById(Guid id)
        {
            var refunds = await _refundService.GetByIdAsync(id);
            return Ok(refunds);
        }

        [HttpPost("/payment")]
        public async Task<ActionResult<RefundResponse>> RefundByPaymentID(RefundRequest request)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var response = await _refundService.RefundByTransID(request.transId, request.IsFullRefund, clientIp, email, true);
            return Ok(response);
        }
        [HttpPost("/pending/{bookingID}")]
        [Authorization("STAFF","ADMIN")]
        public async Task<ActionResult<IEnumerable<RefundResponse>>> RefundByBookingID(Guid bookingID)
        {
            var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
            var email = Request.HttpContext.Items["Email"]?.ToString();
            var response = await _refundService.RefundPending(bookingID, clientIp, email);
            return Ok(response);
        }

        //[HttpPost("/session-order")]
        //public async Task<ActionResult<(IEnumerable<RefundResponse> refundResponses, IEnumerable<PaymentResponse> failPayment)>> RefundByOrderID(RefundRequest request)
        //{
        //    var clientIp = IpAddressHelper.GetClientIpAddress(HttpContext);
        //    var response = await _refundService.RefundByOrderId(request.Id, request.IsFullRefund, clientIp);

        //    return Ok( new { refund});
        //}
    }
}
