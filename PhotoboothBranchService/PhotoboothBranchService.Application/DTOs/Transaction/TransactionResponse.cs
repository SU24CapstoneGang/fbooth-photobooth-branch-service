using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Transaction
{
    public class TransactionResponse
    {
        public Guid TransactionID { get; set; }
        public string GatewayTransactionID { get; set; } = default!;//Transaction ID third party return
        public DateTime TransactionDateTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; } = default!;
        public TransactionStatus TransactionStatus { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid BookingID { get; set; }
    }
}
