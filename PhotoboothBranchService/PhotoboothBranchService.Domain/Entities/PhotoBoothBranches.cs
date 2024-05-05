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
    public String BranchName { get; } = null;
    public String BranchAddress { get; } = null;
    public String EmailAddress { get; } = null;
    public String PhoneNumber { get; } = null;
    public ManufactureStatus Status { get; }
    public virtual Accounts Account { get; }

    public PhotoBoothBranches(Guid id, String branchName, String branchAddress, String emailAddress, String phoneNumber, ManufactureStatus manufactureStatus) {
        BranchName = branchName;
        BranchAddress = branchAddress;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Status = manufactureStatus;
    }
}
