namespace PhotoboothBranchService.Application.DTOs.VNPayPayment
{
    public class VnpayRefundRequest
    {
        public string PaymentID { get; set; }
        public string RefundCategory { get; set; }
        public long Amount { get; set; }
        public string TransId { get; set; }
        public DateTime PayDate { get; set; }
        public string User { get; set; }
    }
}
