using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Discount
{
    public class DiscountFilter
    {
        public string? DiscountCode { get; set; }
        public int? RemaniningUsage { get; set; }
        [Range(0, 90)]
        public Decimal? DiscountRate { get; set; }
        public DiscountStatus? Status { get; set; }
    }
}
