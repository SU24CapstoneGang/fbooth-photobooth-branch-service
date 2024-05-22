using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class PaymentMethod
    {
        public Guid PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public DateTime CreateDate { get; set; }
        public PaymentStatus Status { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
