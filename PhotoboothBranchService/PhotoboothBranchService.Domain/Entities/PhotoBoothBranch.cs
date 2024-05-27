﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class PhotoBoothBranch
{
    public Guid PhotoBoothBranchID { get; set; } = default!;
    public string BranchName { get; set; } = default!;
    public string BranchAddress { get; set; } = default!;
    public DateTime CreateDate { get; set; }
    public ManufactureStatus Status { get; set; } = default!;
    public Guid? AccountID { get; set; }
    public virtual Account Account { get; set; }
    public Guid? CameraID { get; set; }
    public virtual Camera Camera { get; set; }
    public Guid? PrinterID { get; set; }
    public virtual Printer Printer { get; set; }
    public virtual List<Session> Sessions { get; set; }
}
