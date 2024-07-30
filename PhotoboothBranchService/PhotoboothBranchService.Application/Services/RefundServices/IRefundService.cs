using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Application.DTOs.Refund;
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
        Task<RefundResponse> RefundByPaymentID(Guid paymentId, bool isFullRefund, string ipAddress);
        Task<(IEnumerable<RefundResponse> refundResponses, IEnumerable<TransactionResponse> failPayment)> RefundByOrderId(Guid orderId, bool isFullRefund, string? ipAddress);
    }
}
