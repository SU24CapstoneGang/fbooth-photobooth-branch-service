namespace PhotoboothBranchService.Application.DTOs.Transaction
{
    public class CreateTransactionRequest
    {
        public string Description { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid BookingID { get; set; }
        public string? BankCode { get; set; }
        public string ReturnUrl { get; set; }
    }
}
