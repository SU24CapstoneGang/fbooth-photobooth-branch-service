using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PaymentServices
{
    public interface IPaymentService : IServiceBase<PaymentResponse, PaymentFilter, PagingModel>
    {
        public Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest createModel, string ClientIpAddress);
        public Task UpdateAsync(Guid id, UpdatePaymentRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
