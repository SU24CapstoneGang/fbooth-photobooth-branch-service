using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.PaymentMethodServices
{
    public interface IPaymentMethodService : IServiceBase<PaymentMethodResponse, PaymentMethodFilter, PagingModel>
    {
        Task<IEnumerable<PaymentMethodResponse>> GetByName(string name);
        public Task<CreatePaymentMethodResponse> CreateAsync(CreatePaymentMethodRequest createModel);
        public Task UpdateAsync(Guid id, UpdatePaymentMethodRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
