using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class BoothDashboardResponse
    {
        public int TotalBooth { get; set; } = 0;
        public int BoothActive { get; set; } = 0;
        public int BoothInactive { get; set; } = 0;
        public int BoothMaintenance { get; set; } = 0;
        public int BoothInUse { get; set; } = 0;
    }
}
