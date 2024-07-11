using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PaymentServices
{
    public interface IPaymentService : IServiceBase<PaymentResponse, PaymentFilter, PagingModel>
    {
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest createModel, string ClientIpAddress);
        Task UpdateAsync(Guid id, UpdatePaymentRequest updateModel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<PaymentResponse>> GetBySessionOrderAsync(Guid sessionOrderID);
        Task RefundByPaymentID(Guid id, bool isFullRefund, string ipAddress);
    }
}
