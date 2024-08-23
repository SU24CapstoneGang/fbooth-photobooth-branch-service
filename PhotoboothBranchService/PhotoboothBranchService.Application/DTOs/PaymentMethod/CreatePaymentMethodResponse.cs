using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class CreatePaymentMethodResponse
    {
        public Guid PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public PaymentMethodStatus Status { get; set; }
    }
}
