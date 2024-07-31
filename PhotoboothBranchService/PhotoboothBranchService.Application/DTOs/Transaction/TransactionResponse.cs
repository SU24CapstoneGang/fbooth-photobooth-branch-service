using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Transaction
{
    public class TransactionResponse
    {
        public Guid PaymentID { get; set; }
        public string TransactionID { get; set; } = default!;//Transaction ID third party return
        public DateTime PaymentDateTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; } = default!;
        public string ClientIpAddress { get; set; } = default!;
        public PaymentStatus PaymentStatus { get; set; }
        public string Signature { get; set; } = default!;
        public Guid PaymentMethodID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
