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
        public string GatewayTransactionID { get; set; }
        public DateTime RefundDateTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string ResponseMessage { get; set; }
        public RefundStatus Status { get; set; }
        public Guid TransactionID { get; set; }
        public Transaction Transaction { get; set; }
    }
}
