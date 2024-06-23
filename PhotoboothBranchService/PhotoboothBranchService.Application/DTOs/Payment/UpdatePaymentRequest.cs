using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Payment
{
    public class UpdatePaymentRequest
    {
        public string TransactionID { get; set; } = default!;//Transaction ID third party return
        public DateTime PaymentDateTime { get; set; }
        public string Description { get; set; } = default!;
        public PaymentStatus PaymentStatus { get; set; }
        public string Signature { get; set; } = default!;
    }
}
