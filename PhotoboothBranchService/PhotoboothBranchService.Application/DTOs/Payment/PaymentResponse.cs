using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Transaction
{
    public class PaymentResponse
    {
        public Guid PaymentID { get; set; }
        public string TransactionID { get; set; } = default!;
        public DateTime PaymentDateTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; } = default!;
        public TransactionStatus Status { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid BookingID { get; set; }
    }
}
