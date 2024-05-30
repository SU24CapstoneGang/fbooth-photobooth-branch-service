using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class UpdatePaymentMethodRequest
    {
        public string PaymentMethodName { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
