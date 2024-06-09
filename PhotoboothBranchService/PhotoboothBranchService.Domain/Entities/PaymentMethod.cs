using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class PaymentMethod
    {
        public Guid PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public PaymentMethodStatus Status { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
