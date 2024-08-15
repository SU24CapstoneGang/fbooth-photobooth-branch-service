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
        Task SendCancelBookingInformation(Guid bookingID);
        Task SendAutoRegistEmailNoti(string email, string link, string customerName);
        Task SendResetPasswordEmail(string email, string resetLink, string customerName);
    }
}
