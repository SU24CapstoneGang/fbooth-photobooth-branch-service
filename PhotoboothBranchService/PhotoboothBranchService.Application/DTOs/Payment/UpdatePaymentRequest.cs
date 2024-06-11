using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Payment
{
    public class UpdatePaymentRequest
    {
        public string Description { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
