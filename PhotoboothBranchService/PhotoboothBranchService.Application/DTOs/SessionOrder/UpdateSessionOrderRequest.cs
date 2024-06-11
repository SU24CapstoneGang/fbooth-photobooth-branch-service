using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class UpdateSessionOrderRequest
    {
        public double TotalPrice { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
        public SessionOrderStatus Status { get; set; }
    }
}
