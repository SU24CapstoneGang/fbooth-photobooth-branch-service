using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;

namespace PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices
{
    public interface IVNPayService
    {
        string Pay(VnpayRequest paymentRequest);
        Task<VnpayQueryResponse> Query(Guid paymentID, string payDate, string clientIp);
        Task<VnpayRefundResponse> RefundTransaction(VnpayRefundRequest request, string clientIp);
        Task<(VnpayResponse response, string returnContent)> Return(IQueryCollection queryString);
    }
}
