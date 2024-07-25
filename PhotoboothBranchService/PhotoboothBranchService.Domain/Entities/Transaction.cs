using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Transaction
    {
        public Guid TransactionID { get; set; }
        public string GatewayTransactionID { get; set; } = default!;//Transaction ID third party return
        public DateTime TransactionDateTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; } = default!;
        public PaymentStatus PaymentStatus { get; set; }
        public Guid PaymentMethodID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; } = default!;
        public Guid SessionOrderID { get; set; }
        public virtual Booking SessionOrder { get; set; } = default!;
        public virtual ICollection<Refund> Refunds { get; set; }
    }
}
