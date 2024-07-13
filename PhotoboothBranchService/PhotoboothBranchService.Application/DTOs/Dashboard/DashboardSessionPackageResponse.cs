using PhotoboothBranchService.Application.DTOs.SessionPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class DashboardSessionPackageResponse
    {
        public int Count { get; set; }
        public SessionPackageResponse SessionPackage { get; set; }
    }
}
