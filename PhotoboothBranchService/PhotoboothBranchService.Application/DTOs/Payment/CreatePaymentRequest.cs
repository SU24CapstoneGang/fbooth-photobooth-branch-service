namespace PhotoboothBranchService.Application.DTOs.Payment
{
    public class CreatePaymentRequest
    {
        public string Description { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid SessionOrderID { get; set; }
        public string? BankCode { get; set; }
        public PayType PayType { get; set; }
    }

    public enum PayType
    {
        Deposit = 1,
        FullPay = 2,
    }
}
