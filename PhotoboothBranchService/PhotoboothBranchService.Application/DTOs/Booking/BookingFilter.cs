using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class BookingFilter
    {
        public double? TotalPrice { get; set; } = default!;
        public BookingStatus? Status { get; set; }
        public Guid? BoothID { get; set; }
        public Guid? CustomerID { get; set; }
    }
}
