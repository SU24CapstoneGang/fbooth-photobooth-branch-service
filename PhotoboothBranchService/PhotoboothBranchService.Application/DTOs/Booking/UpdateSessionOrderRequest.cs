using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class UpdateSessionOrderRequest
    {
        public Guid? BoothID { get; set; }
        public DateTime? StartTime { get; set; } = default!;
    }
}
