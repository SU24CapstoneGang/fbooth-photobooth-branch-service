using PhotoboothBranchService.Application.DTOs.Background;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class DashboardBackgroundResponse
    {
        public int Count { get; set; }
        public BackgroundResponse Background { get; set; }
    }
}
