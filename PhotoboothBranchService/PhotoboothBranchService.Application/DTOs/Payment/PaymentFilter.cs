using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Transaction
{
    public class PaymentFilter
    {
        public string? TransactionID { get; set; } //Transaction ID third party return
        public DateTime? PaymentDateTime { get; set; }
        public string? Description { get; set; }
        public TransactionStatus? Status { get; set; }
        public Guid? PaymentMethodID { get; set; }
        public Guid? BookingID { get; set; }
    }
}
