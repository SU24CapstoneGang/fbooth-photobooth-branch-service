using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;

namespace PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices
{
    public interface IVNPayService
    {
        Task<string> Pay(VnpayRequest paymentRequest);
        Task<VnpayQueryResponse> Query(string orderId, string payDate, string clientIp);
        Task<VnpayRefundResponse> RefundTransaction(VnpayRefundRequest request, string clientIp);
        VnpayResponse Return(IQueryCollection queryString);
    }
}
