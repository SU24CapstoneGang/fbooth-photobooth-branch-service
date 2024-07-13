using PhotoboothBranchService.Application.DTOs.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class DashboardLayoutResponse
    {
        public int Count { get; set; }
        public LayoutResponse Layout { get; set; }
    }
}
