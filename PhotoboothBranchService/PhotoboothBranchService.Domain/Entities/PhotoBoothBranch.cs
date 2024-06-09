using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class PhotoBoothBranch
{
    public Guid PhotoBoothBranchID { get; set; } 
    public string BranchName { get; set; } = default!;
    public string BranchAddress { get; set; } = default!;
    public DateTime CreateDate { get; set; }
    public ManufactureStatus Status { get; set; } = default!;
    public Guid? AccountID { get; set; }
    public virtual Account Account { get; set; }
    public virtual ICollection<SessionOrder> SessionOrders { get; set; }
    public virtual ICollection<Booth> Booths { get; set; }
}
