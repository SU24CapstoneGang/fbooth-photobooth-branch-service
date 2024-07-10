using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.Payment.MoMoPayment;

namespace PhotoboothBranchService.Application.Services.PaymentServices.MoMoServices
{
    public interface IMoMoService
    {
        string CreatePayment(MoMoRequest request);
        Task HandlePaymentResponeIPN(MoMoResponse momoResponse);
        Task Return(IQueryCollection queryString);
        Task<MoMoRefundResponse> RefundById(Guid paymentID, bool isFullRefund);
    }
}
