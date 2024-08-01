namespace PhotoboothBranchService.Application.DTOs.VNPayPayment
{
    public class VnpayRequest
    {
        public Guid PaymentID { get; set; }
        public long Amount { get; set; }
        public string? BankCode { get; set; }
        public Guid BookingID { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public string? OrderInformation { get; set; }
        public string ClientIpAddress { get; set; }
        public string ReturnUrl { get; set; }
    }
}
