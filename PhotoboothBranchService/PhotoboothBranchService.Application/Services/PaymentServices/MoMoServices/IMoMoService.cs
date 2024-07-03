using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.Payment.MoMoPayment;

namespace PhotoboothBranchService.Application.Services.PaymentServices.MoMoServices
{
    public interface IMoMoService
    {
        public string CreatePayment(MoMoRequest request);
        public Task HandlePaymentResponeIPN(MoMoResponse momoResponse);
        Task Return(IQueryCollection queryString);
    }
}
