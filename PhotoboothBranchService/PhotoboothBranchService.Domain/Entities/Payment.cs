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
        public string TransactionID { get; set; } //Transaction ID third party return
        public DateTime PaymentDateTime { get; set; }
        public string Description { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string Signature { get; set; }
        public Guid PaymentMethodID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public Guid SessionOrderID { get; set; }
        public virtual SessionOrder SessionOrder { get; set; }
    }
}
