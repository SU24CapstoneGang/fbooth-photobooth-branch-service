using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class SessionOrder
    {
        public Guid SessionOrderID { get; set; } = default!;
        public double TotalPrice { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime? EndTime { get; set; } = default!;
        public SessionOrderStatus Status { get; set; }
        public Guid BoothBranchID { get; set; }
        public virtual BoothBranch BoothBranch { get; set; }
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; }
        public Guid? AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}
