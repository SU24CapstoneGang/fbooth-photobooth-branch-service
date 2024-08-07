using PhotoboothBranchService.Application.DTOs.BookingService;
using PhotoboothBranchService.Application.DTOs.FullPaymentPolicy;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class BookingResponse
    {
        public Guid BookingID { get; set; }
        public string CustomerReferenceID { get; set; } = default!;
        public long ValidateCode { get; set; }
        public decimal HireBoothFee { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingType BookingType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public BookingStatus Status { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public decimal? RefundAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid BoothID { get; set; }
        public Guid CustomerID { get; set; }
        public FullPaymentPolicyResponse FullPaymentPolicy { get; set; }
        public List<BookingServiceResponse> BookingServices { get; set; } = new List<BookingServiceResponse>();
    }
}
