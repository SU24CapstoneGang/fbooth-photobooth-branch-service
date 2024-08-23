using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class BookingFilter
    {
        public double? TotalPrice { get; set; } = default!;
        public string? CustomerBusinessID { get; set; }
        public BookingStatus? BookingStatus { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }
        public Guid? BoothID { get; set; }
        public Guid? CustomerID { get; set; }
    }
}
