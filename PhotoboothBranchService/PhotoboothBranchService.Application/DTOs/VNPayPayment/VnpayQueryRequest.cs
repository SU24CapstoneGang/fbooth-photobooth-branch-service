namespace PhotoboothBranchService.Application.DTOs.VNPayPayment
{
    public class VnpayQueryRequest
    {
        public Guid PaymentID { get; set; }
        public string PayDate { get; set; }
    }
}
