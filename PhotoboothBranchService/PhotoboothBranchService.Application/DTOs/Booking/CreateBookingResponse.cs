using PhotoboothBranchService.Application.DTOs.BookingService;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class CreateBookingResponse
    {
        public Guid BookingID { get; set; }
        public string CustomerReferenceID { get; set; } = default!;
        public decimal HireBoothFee { get; set; }
        public decimal TotalPrice { get; set; } = default!;
        public decimal PaidAmount { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingType BookingType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid BoothID { get; set; }
        public Guid CustomerID { get; set; }
        public virtual ICollection<BookingServiceResponse> BookingServices { get; set; } = default!;
    }
}
