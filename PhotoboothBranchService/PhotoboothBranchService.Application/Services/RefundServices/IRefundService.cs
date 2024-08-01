﻿using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Refund;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.RefundServices
{
    public interface IRefundService : IServiceBase<RefundResponse, RefundFilter, PagingModel>
    {
        Task<RefundResponse> RefundByTransID(Guid paymentId, bool isFullRefund, string ipAddress);
        Task<(IEnumerable<RefundResponse> refundResponses, IEnumerable<TransactionResponse> failPayment)> RefundByBookingID(Guid orderId, bool isFullRefund, string? ipAddress);
    }
}
