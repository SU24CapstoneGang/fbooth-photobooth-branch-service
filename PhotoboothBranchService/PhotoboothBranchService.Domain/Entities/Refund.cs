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
        public string TransactionID { get; set; }
        public DateTime RefundDateTime { get; set; }
        public string Description { get; set; }
        public RefundStatus Status { get; set; }
        public Guid PaymentID { get; set; }
        public Payment Payment { get; set; }
    }
}
