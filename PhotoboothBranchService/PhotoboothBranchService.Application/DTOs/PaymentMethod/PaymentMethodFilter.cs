using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class PaymentMethodFilter
    {
        public Guid? PaymentMethodID { get; set; }
        public string? PaymentMethodName { get; set; }
        public DateTime? CreateDate { get; set; }
        public PaymentStatus? Status { get; set; }
    }
}
