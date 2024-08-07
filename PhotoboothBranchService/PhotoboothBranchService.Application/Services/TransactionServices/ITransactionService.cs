﻿using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Application.DTOs.VNPayPayment;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.TransactionServices
{
    public interface ITransactionService : IServiceBase<TransactionResponse, PaymentFilter, PagingModel>
    {
        Task<CreateTransactionResponse> CreateAsync(CreateTransactionRequest createModel, string ClientIpAddress, string? email);
        Task UpdateAsync(Guid id, UpdateTransactiontRequest updateModel);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TransactionResponse>> GetByBookingAsync(Guid sessionOrderID);
        Task<IEnumerable<TransactionResponse>> GetCustomerTransaction(string? email);
        Task HandleMomoResponse(IQueryCollection queryString);
        Task<MomoIPNResponse> HandleMomoIPN(MoMoResponse moMoResponse);
        Task<(VnpayResponse response, string returnContent)> HandleVnpayResponse(IQueryCollection queryString);
    }
}
