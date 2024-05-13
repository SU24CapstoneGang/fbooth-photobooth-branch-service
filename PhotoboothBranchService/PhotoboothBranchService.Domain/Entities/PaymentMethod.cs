using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class PaymentMethod
    {
        public Guid PaymentID { get; set; }
        public string PaymentName { get; set; }
        public DateTime CreateDate { get; set; }
        public PaymentStatus Status { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
