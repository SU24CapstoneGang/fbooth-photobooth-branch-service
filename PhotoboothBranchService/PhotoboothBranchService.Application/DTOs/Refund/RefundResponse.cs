using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Refund
{
    public class RefundResponse
    {
        public Guid RefundID { get; set; }
        public string TransactionID { get; set; }
        public DateTime RefundDateTime { get; set; }
        public string Description { get; set; }
        public RefundStatus Status { get; set; }
        public Guid PaymentID { get; set; }
    }
}
