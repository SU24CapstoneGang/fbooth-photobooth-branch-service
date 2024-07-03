using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class SessionOrder
    {
        public Guid SessionOrderID { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime? EndTime { get; set; } = default!;
        public SessionOrderStatus Status { get; set; }
        public long ValidateCode { get; set; }
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; } = default!;
        public Guid? AccountID { get; set; }
        public virtual Account Account { get; set; } = default!;
        public Guid SessionPackageID { get; set; }
        public virtual SessionPackage SessionPackage { get; set; } = default!;
        public virtual ICollection<Payment> Payments { get; set; } = default!;
        public virtual ICollection<ServiceItem> ServiceItems { get; set; } = default!;
        public virtual ICollection<PhotoSession> PhotoSessions { get; set; } = default!;
    }
}
