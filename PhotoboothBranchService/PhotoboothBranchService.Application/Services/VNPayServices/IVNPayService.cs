using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.VNPayPayment;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.Services.VNPayServices
{
    public interface IVNPayService
    {
        string Pay(VnpayRequest paymentRequest);
        Task<VnpayQueryResponse> Query(Guid paymentID, string payDate, string clientIp);
        Task<VnpayRefundResponse> RefundTransaction(VnpayRefundRequest request, string clientIp);
        Task<(VnpayResponse response, string returnContent, Transaction paymentResult)> Return(IQueryCollection queryString);
    }
}
