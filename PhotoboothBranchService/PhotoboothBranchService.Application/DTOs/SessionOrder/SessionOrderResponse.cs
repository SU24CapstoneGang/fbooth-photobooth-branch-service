using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class SessionOrderResponse
    {
        public Guid SessionOrderID { get; set; }
        public double TotalPrice { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public SessionOrderStatus Status { get; set; }
        public Guid BranchesID { get; set; }
        public Guid BoothBranchId { get; set; }
        public Guid AccountID { get; set; }
    }
}
