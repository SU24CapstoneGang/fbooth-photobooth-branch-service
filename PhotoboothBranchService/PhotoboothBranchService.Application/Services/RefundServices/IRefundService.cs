using PhotoboothBranchService.Application.DTOs;
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
        Task RefundByPaymentID(Guid id, bool isFullRefund, string ipAddress);
    }
}
