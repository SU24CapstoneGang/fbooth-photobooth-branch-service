using PhotoboothBranchService.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.DashboardServices
{
    public interface IDashboardService
    {
        Task<BasicBranchDashboardResponse> BasicBranchDashboard(Guid branchID);
        Task<BasicDashboardResponse> BasicDashboard();
        Task<BookingDashboardResponse> BookingDashboard(Guid? branchID, DateOnly? startDate, DateOnly? endDate);
        Task<List<DashboardServiceResponse>> DashboradService(Guid? branchID, DateOnly? startDate, DateOnly? endDate);
        Task<List<DashboardLayoutResponse>> DashboardLayout(Guid? branchID, DateOnly? startDate, DateOnly? endDate);
        Task<List<DashboardBackgroundResponse>> DashboardBackground(Guid? branchID, DateOnly? startDate, DateOnly? endDate);
    }
}
