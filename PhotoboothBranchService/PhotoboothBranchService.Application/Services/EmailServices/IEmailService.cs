using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendBillInformation(Guid paymentId);
        Task SendBookingInformation(Guid sessionOrderId);
    }
}
