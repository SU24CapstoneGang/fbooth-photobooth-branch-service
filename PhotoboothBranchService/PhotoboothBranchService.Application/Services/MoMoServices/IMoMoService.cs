using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.Services.MoMoServices
{
    public interface IMoMoService
    {
        string CreatePayment(MoMoRequest request);
        Task<(Payment transaction, MomoIPNResponse iPNResponse)> HandlePaymentResponeIPN(MoMoResponse momoResponse);
        Task<Payment> Return(IQueryCollection queryString);
        Task<MoMoRefundResponse> RefundById(Guid paymentID, long refundAmounf, string description);
    }
}
