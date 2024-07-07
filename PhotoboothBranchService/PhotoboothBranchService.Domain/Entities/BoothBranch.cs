﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class BoothBranch
{
    public Guid BoothBranchID { get; set; }
    public string BranchName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Town { get; set; } = default!;
    public string City { get; set; } = default!;
    public DateTime CreateDate { get; set; }
    public ManufactureStatus Status { get; set; } = default!;
    public Guid ManagerID { get; set; }
    public virtual Account Manager { get; set; } = default!;
    public virtual ICollection<Booth> Booths { get; set; } = default!;
    public virtual ICollection<Account> Staffs { get; set; } = default!;
}
