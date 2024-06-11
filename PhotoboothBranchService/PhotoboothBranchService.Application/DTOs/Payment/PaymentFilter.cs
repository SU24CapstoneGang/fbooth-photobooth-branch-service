using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Payment
{
    public class PaymentFilter
    {
        public string? TransactionID { get; set; } //Transaction ID third party return
        public DateTime? PaymentDateTime { get; set; }
        public string? Description { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public string? Signature { get; set; }
        public Guid? PaymentMethodID { get; set; }
        public Guid? SessionOrderID { get; set; }
    }
}
