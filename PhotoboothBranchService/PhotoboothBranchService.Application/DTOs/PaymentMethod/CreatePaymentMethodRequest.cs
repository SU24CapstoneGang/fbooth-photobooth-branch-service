using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class CreatePaymentMethodRequest
    {
        [Required]
        public string PaymentMethodName { get; set; } = default!;
        [Required]
        public IFormFile IconImage { get; set; } = default!;
        [Required]
        public PaymentMethodStatus Status { get; set; }
    }
}
