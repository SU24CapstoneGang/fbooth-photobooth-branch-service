using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendRefundBillInformation(Guid refundId);
        Task SendBookingInformation(Guid sessionOrderId, Guid transactionID);
    }
}
