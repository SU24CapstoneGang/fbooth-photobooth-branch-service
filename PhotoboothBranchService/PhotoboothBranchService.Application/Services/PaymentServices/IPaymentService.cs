using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Application.DTOs.VNPayPayment;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.TransactionServices
{
    public interface IPaymentService : IServiceBase<PaymentResponse, PaymentFilter, PagingModel>
    {
        Task<CreatePaymentResponse> CreateAsync(CreatePaymentRequest createModel, string ClientIpAddress, string? email);
        Task UpdateAsync(Guid id, UpdatePaymentRequest updateModel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<PaymentResponse>> GetByBookingAsync(Guid sessionOrderID);
        Task<IEnumerable<PaymentResponse>> GetCustomerTransaction(string? email);
        Task HandleMomoResponse(IQueryCollection queryString);
        Task<MomoIPNResponse> HandleMomoIPN(MoMoResponse moMoResponse);
        Task<(VnpayResponse response, string returnContent)> HandleVnpayResponse(IQueryCollection queryString);
        Task<IEnumerable<PaymentResponse>> StaffGetBranchTransaction(string? email);
        Task<IEnumerable<PaymentResponse>> GetBranchTransaction(Guid branchID);
    }
}
