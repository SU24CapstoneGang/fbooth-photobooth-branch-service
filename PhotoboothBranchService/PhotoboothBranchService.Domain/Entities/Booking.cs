using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Booking
    {
        public Guid BookingID { get; set; } = default!;
        public string CustomerReferenceID { get; set; } = default!;
        public long ValidateCode { get; set; }
        public decimal PaymentAmount { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
        public BookingType BookingType { get; set; } = default!;
        public PaymentStatus PaymentStatus { get; set; }
        public BookingStatus Status { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public decimal HireBoothFee { get; set; }
        public decimal? RefundAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? FullPaymentPolicyID { get; set; }
        public virtual FullPaymentPolicy FullPaymentPolicy { get; set; } = default!;
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; } = default!;
        public Guid CustomerID { get; set; }
        public virtual Account Account { get; set; } = default!;
        public virtual ICollection<Transaction> Payments { get; set; } = default!;
        public virtual ICollection<BookingService> BookingServices { get; set; } = default!;
        public virtual ICollection<PhotoSession> PhotoSessions { get; set; } = default!;
    }
}
