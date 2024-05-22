using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Application.DTOs.PrintPricing;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PaymentMethodServices
{
    public interface IPaymentMethodService : IService<PaymentMethodResponse, CreatePaymentMethodRequest, UpdatePaymentMethodRequest, PaymentMethodFilter, PagingModel>
    {
        Task<IEnumerable<PaymentMethodResponse>> GetByName(string name);
    }
}
