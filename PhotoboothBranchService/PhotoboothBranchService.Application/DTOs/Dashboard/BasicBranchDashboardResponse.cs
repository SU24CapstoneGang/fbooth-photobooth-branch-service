using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class BasicBranchDashboardResponse
    {
        public BoothDashboardResponse BoothDashboard { get; set; } = new BoothDashboardResponse();
        public StaffDashboardResponse StaffDashboard { get; set; } = new StaffDashboardResponse();
        public int TotalOrder { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
        public int CountCustomer { get; set; } = 0;
    }
}
