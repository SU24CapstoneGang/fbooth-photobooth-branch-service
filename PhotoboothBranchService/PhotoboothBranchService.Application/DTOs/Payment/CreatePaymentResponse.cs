using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Payment
{
    public class CreatePaymentResponse
    {
        public string PaymentUlr { get; set; }
        public Guid PaymentID { get; set; }

    }
}
