using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.PrintPricing
{
    public class PrintPricingResponse
    {
        public Guid PrintPricingID { get; set; }
        public float UnitPrice { get; set; }
        public string PrintName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
