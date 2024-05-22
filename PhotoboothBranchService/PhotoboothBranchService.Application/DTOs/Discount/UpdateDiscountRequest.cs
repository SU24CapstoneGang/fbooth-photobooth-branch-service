using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Discount;

public class UpdateDiscountRequest
{
    public int RemaniningUsage { get; set; }
    public decimal DiscountRate { get; set; }
    public DiscountStatus Status { get; set; }
}
