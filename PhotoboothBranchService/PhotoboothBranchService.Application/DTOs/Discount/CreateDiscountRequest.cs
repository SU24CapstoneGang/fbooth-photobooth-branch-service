using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Discount;

public class CreateDiscountRequest
{
    public string DiscountCode { get; set; }
    public int RemaniningUsage { get; set; }
    [Range(0, 90)]
    public decimal DiscountRate { get; set; }
    public DiscountStatus Status { get; set; }
}
