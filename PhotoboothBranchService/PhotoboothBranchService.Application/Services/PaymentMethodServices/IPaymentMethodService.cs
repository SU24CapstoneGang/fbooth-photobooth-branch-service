﻿using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.PaymentMethodServices
{
    public interface IPaymentMethodService : IServiceBase<PaymentMethodResponse, PaymentMethodFilter, PagingModel>
    {
        Task<IEnumerable<PaymentMethodResponse>> GetByName(string name);
        Task<CreatePaymentMethodResponse> CreateAsync(CreatePaymentMethodRequest createModel);
        Task UpdateAsync(Guid id, UpdatePaymentMethodRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
