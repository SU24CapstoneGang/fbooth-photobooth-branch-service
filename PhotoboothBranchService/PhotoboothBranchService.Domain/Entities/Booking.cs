using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Booking
    {
        public Guid BookingID { get; set; } = default!;
        public string CustomerBusinessID { get; set; } = default!;
        public long ValidateCode { get; set; }
        public decimal TotalPrice { get; set; } = default!;
        public decimal PaidAmount { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
        public BookingType BookingType { get; set; } = default!;
        public PaymentStatus PaymentStatus { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public DateTime? CancelledDate { get; set; }
        public decimal HireBoothFee { get; set; }
        public decimal RefundedAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; } = default!;
        public Guid CustomerID { get; set; }
        public virtual Account Account { get; set; } = default!;
        public virtual ICollection<Payment> Payments { get; set; } = default!;
        public virtual ICollection<BookingService> BookingServices { get; set; } = default!;
        public virtual ICollection<PhotoSession> PhotoSessions { get; set; } = default!;
        public virtual ICollection<BookingSlot> BookingSlots { get; set; } = default!;
    }
}
