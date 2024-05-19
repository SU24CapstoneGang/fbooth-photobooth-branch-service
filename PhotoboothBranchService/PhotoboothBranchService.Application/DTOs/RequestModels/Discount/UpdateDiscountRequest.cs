using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Discount;

public class UpdateDiscountRequest
{
    public int RemaniningUsage { get; set; }
    public Decimal DiscountRate { get; set; }
    public DiscountStatus Status { get; set; }
}
