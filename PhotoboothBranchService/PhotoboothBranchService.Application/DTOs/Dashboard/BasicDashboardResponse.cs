﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class BasicDashboardResponse
    {
        public int TotalBranch { get; set; } = 0;
        public BoothDashboardResponse BoothDashboard { get; set; } = new BoothDashboardResponse();
        public StaffDashboardResponse StaffDashboard { get; set; } = new StaffDashboardResponse();
        public int TotalBookings { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
        public long CountCustomer { get; set; } = 0;
    }
}
