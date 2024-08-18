using PhotoboothBranchService.Application.DTOs;
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
        Task<RefundResponse> RefundByTransID(Guid transId, bool isFullRefund, string? ipAddress, string? email, bool isRecord);
        Task<IEnumerable<RefundResponse>> RefundByBookingID(Guid orderId, bool isFullRefund, string? ipAddress, string? email);
        Task<IEnumerable<RefundResponse>> RefundPending(Guid bookingId, string? ipAddress, string? email);
    }
}
