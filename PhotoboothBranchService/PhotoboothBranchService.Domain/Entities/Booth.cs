using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Booth
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public ManufactureStatus Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid BranchID { get; set; }
        public virtual Branch Branch { get; set; } = default!;
        public virtual ICollection<SessionOrder> SessionOrders { get; set; } = default!;
        public virtual ICollection<Device> Devices { get; set; } = default!;
    }
}
