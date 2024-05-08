using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class PhotoBoothBranches : Entity
{
    public string BranchName { get; } = null;
    public string BranchAddress { get; } = null;
    public ManufactureStatus Status { get; }
    public virtual Accounts Account { get; }
    public virtual Cameras Camera { get; }
    public virtual Printers Printer { get; }
    public PhotoBoothBranches(Guid id, string branchName, string branchAddress, ManufactureStatus manufactureStatus) : base(id)
    {
        BranchName = branchName;
        BranchAddress = branchAddress;
        Status = manufactureStatus;
    }

    public PhotoBoothBranches(string branchName, string branchAddress, ManufactureStatus status, Accounts account, Cameras camera, Printers printer)
    {
        BranchName = branchName;
        BranchAddress = branchAddress;
        Status = status;
        Account = account;
        Camera = camera;
        Printer = printer;
    }
    private PhotoBoothBranches() { }
}
