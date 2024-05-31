using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class PaymentMethodResponse
    {
        public Guid PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public DateTime CreateDate { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
