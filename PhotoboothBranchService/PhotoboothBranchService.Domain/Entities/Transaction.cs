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
        public TransactionStatus TransactionStatus { get; set; }
        public Guid PaymentMethodID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; } = default!;
        public Guid BookingID { get; set; }
        public virtual Booking Booking { get; set; } = default!;
        public virtual ICollection<Refund> Refunds { get; set; }
    }
}
