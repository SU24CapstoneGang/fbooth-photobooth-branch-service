using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Refund
{
    public class RefundResponse
    {
        public Guid RefundID { get; set; }
        public string TransactionID { get; set; }
        public DateTime RefundDateTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string ResponseMessage { get; set; }
        public RefundStatus Status { get; set; }
        public Guid PaymentID { get; set; }
    }
}
