using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Booth
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
        public virtual BoothBranch PhotoBoothBranch { get; set; } = default!;
        public virtual ICollection<SessionOrder> SessionOrders { get; set; } = default!;
        public virtual ICollection<Device> Devices { get; set; } = default!;
    }
}
