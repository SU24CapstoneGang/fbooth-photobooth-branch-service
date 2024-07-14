using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class BasicBranchDashboardResponse
    {
        public int TotalBooth {  get; set; }
        public int TotalStaff { get; set; }
        public int TotalOrder { get; set; }
        public decimal TotalRevenue { get; set; }
        public int CountCustomer { get; set; }
    }
}
