namespace PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment
{
    public class VnpayRefundRequest
    {
        public string RefundCategory { get; set; }
        public long Amount { get; set; }
        public string SessionId { get; set; }
        public string TransId { get; set; }
        public DateTime PayDate { get; set; }
        public string User { get; set; }
    }
}
