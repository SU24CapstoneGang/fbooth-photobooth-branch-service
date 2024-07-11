using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Application.DTOs.SessionPackage;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class SessionOrderResponse
    {
        public Guid SessionOrderID { get; set; }
        public decimal TotalPrice { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public long ValidateCode { get; set; }
        public SessionOrderStatus Status { get; set; }
        public Guid AccountID { get; set; }
        public Guid BoothID { get; set; }
        public Guid SessionPackageID { get; set; }
        public SessionPackageResponse SessionPackage { get; set; }
        public List<ServiceItemResponse> ServiceItems { get; set; }
    }
}
