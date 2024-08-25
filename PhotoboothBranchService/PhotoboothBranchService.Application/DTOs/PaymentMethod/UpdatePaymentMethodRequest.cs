using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class UpdatePaymentMethodRequest
    {
        public string? PaymentMethodName { get; set; }
        public IFormFile? IconImage { get; set; } = default!;
        public PaymentMethodStatus? Status { get; set; }
    }
}
