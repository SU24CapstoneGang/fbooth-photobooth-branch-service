using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class UpdatePaymentMethodRequest
    {
        public string PaymentMethodName { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
