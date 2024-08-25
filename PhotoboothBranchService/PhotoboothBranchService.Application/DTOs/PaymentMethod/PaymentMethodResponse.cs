using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class PaymentMethodResponse
    {
        public Guid PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public string MethodIconUrl { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public PaymentMethodStatus Status { get; set; }
    }
}
