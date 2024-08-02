using PhotoboothBranchService.Application.DTOs.BookingService;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class CreateBookingResponse
    {
        public Guid BookingID { get; set; }
        public long ValidateCode { get; set; }
        public decimal HireBoothFee { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingType BookingType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public BookingStatus Status { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid BoothID { get; set; }
        public Guid CustomerID { get; set; }
        public virtual ICollection<BookingServiceResponse> BookingServices { get; set; } = default!;
    }
}
