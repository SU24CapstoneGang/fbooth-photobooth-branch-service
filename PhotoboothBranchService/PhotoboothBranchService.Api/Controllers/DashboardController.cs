using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs.Dashboard;
using PhotoboothBranchService.Application.Services.DashboardServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class DashboardController : ControllerBaseApi
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        //basic
        [HttpGet("branch-basic")]
        public async Task<ActionResult<BasicBranchDashboardResponse>> BasicBranchDashboard([FromQuery] Guid branchID)
        {
            return await _dashboardService.BasicBranchDashboard(branchID);
        }
        [HttpGet("basic")]
        public async Task<ActionResult<BasicDashboardResponse>> BasicDashboard()
        {
            return await _dashboardService.BasicDashboard();
        }
        //service
        [HttpGet("branch-service")]
        public async Task<ActionResult<List<DashboardServiceResponse>>> BranchDashboradService([FromQuery] Guid branchID, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboradService(branchID, startDate, endDate);
        }
        [HttpGet("service")]
        public async Task<ActionResult<List<DashboardServiceResponse>>> DashboradService([FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboradService(null, startDate, endDate);
        }
        //session package
        [HttpGet("branch-session-package")]
        public async Task<ActionResult<List<DashboardSessionPackageResponse>>> BranchDashboradSessionPackage([FromQuery] Guid branchID, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboradSessionPackage(branchID, startDate, endDate);
        }
        [HttpGet("session-package")]
        public async Task<ActionResult<List<DashboardSessionPackageResponse>>> DashboradSessionPackage([FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboradSessionPackage(null, startDate, endDate);
        }
        //layout
        [HttpGet("branch-layout")]
        public async Task<ActionResult<List<DashboardLayoutResponse>>> BranchDashboardLayout([FromQuery] Guid branchID, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboardLayout(branchID, startDate, endDate);
        }
        [HttpGet("layout")]
        public async Task<ActionResult<List<DashboardLayoutResponse>>> DashboardLayout([FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboardLayout(null, startDate, endDate);
        }
        //background
        [HttpGet("branch-background")]
        public async Task<ActionResult<List<DashboardBackgroundResponse>>> BranchDashboardBackground([FromQuery] Guid branchID, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboardBackground(branchID, startDate, endDate);
        }
        [HttpGet("background")]
        public async Task<ActionResult<List<DashboardBackgroundResponse>>> DashboardBackground([FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            return await _dashboardService.DashboardBackground(null, startDate, endDate);
        }
    }
}
