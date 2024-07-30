using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class UpdateSessionOrderRequest
    {
        public Guid? BoothID { get; set; }
        public DateTime? StartTime { get; set; } = default!;
    }
}
