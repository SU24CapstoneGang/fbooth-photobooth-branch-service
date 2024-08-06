using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class Branch
{
    public Guid BranchID { get; set; }
    public string BranchName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Town { get; set; } = default!;
    public string City { get; set; } = default!;
    public DateTime CreateDate { get; set; }
    public TimeSpan OpeningTime { get; set; } // Thêm giờ mở cửa
    public TimeSpan ClosingTime { get; set; } // Thêm giờ đóng cửa
    public BranchStatus Status { get; set; } = default!;
    public virtual ICollection<Booth> Booths { get; set; } = default!;
    public virtual ICollection<Account> Staffs { get; set; } = default!;
}
