using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PaymentServices
{
    public interface IPaymentService : IServiceBase<PaymentResponse, CreatePaymentRequest, CreatePaymentResponse, UpdatePaymentRequest, PaymentFilter, PagingModel>
    {
    }
}
