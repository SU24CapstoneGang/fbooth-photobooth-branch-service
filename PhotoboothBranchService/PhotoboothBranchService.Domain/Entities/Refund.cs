using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Refund
    {
        public Guid RefundID { get; set; }
        public string TransactionID { get; set; } = default!;
        public DateTime RefundDateTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; } = default!;
        public string ResponseMessage { get; set; } = default!;
        public RefundStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid PaymentID { get; set; }
        public Payment Payment { get; set; } = default!;
    }
}
