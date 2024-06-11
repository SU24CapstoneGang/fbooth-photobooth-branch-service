namespace PhotoboothBranchService.Application.DTOs.Payment
{
    public class CreatePaymentRequest
    {
        public string Description { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
