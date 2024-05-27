using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class TransactionHistory
    {
        public Guid TransactionID { get; set; }
        public string Description { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public Guid SessionID { get; set; }
        public virtual Session Session { get; set; }
        public Guid PaymentMethodID { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }



    }
}
