using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentID { get; set; }
        public string TransactionID { get; set; } = default!;//Transaction ID third party return
        public DateTime PaymentDateTime { get; set; }
        public string Description { get; set; } = default!;
        public PaymentStatus PaymentStatus { get; set; }
        public string Signature { get; set; } = default!;
        public Guid PaymentMethodID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; } = default!;
        public Guid SessionOrderID { get; set; }
        public virtual SessionOrder SessionOrder { get; set; } = default!;
    }
}
