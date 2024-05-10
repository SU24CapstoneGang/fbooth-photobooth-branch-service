using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class PhotoBoothBranches : BaseEntity
{
    public string BranchName { get; } = null!;
    public string BranchAddress { get; } = null!;
    public ManufactureStatus Status { get; }
    public Guid? AccountId { get; } 
    public Guid? CameraId { get; }
    public Guid? PrinterId { get; }
    public virtual Accounts Account { get; }
    public virtual Cameras Camera { get; }
    public virtual Printers Printer { get; }
    [JsonConstructor]
    public PhotoBoothBranches(Guid id, string branchName, string branchAddress, ManufactureStatus manufactureStatus, Guid accountId, Guid cameraId, Guid printerId) : base(id)
    {
        BranchName = branchName;
        BranchAddress = branchAddress;
        Status = manufactureStatus;
        AccountId = accountId;
        CameraId = cameraId;
        PrinterId = printerId;
    }
    public PhotoBoothBranches(Guid id, string branchName, string branchAddress, ManufactureStatus status, Accounts account, Cameras camera, Printers printer) : base(id)
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
