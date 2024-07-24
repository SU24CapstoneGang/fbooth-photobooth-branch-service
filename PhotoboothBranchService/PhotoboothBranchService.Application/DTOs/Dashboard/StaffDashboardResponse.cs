using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class StaffDashboardResponse
    {
        public int TotalStaff { get; set; } = 0;
        public int StaffActive { get; set; } = 0;
        public int StaffBlocked { get; set; } = 0;
    }
}
