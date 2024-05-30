using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class CreatePaymentMethodRequest
    {
        public string PaymentMethodName { get; set; }
    }
}
