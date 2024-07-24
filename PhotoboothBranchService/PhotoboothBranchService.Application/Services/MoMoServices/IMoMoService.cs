﻿using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.Services.MoMoServices
{
    public interface IMoMoService
    {
        string CreatePayment(MoMoRequest request);
        Task<Transaction> HandlePaymentResponeIPN(MoMoResponse momoResponse);
        Task<Transaction> Return(IQueryCollection queryString);
        Task<MoMoRefundResponse> RefundById(Guid paymentID, bool isFullRefund);
    }
}
