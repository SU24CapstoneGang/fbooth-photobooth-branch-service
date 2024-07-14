using PhotoboothBranchService.Application.DTOs.Service;

namespace PhotoboothBranchService.Application.DTOs.Dashboard
{
    public class DashboardServiceResponse
    {
        public int Quantity {  get; set; }
        public ServiceResponse Service { get; set; }
    }
}
