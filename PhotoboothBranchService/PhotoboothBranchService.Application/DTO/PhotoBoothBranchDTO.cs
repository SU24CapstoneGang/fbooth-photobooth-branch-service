using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class PhotoBoothBranchDTO
{
    public Guid? PhotoBoothBranchId { get; set; }
    public string BranchName { get; } = null;
    public string BranchAddress { get; } = null;
    public ManufactureStatus Status { get; }
    public string AccountId { get; set; } = null;
    public Accounts Account { get; }

    //contrustor respone
    public PhotoBoothBranchDTO(Guid? photoBoothBranchId, string branchName, string branchAddress, ManufactureStatus manufactureStatus)
    {
        PhotoBoothBranchId = photoBoothBranchId;
        BranchName = branchName;
        BranchAddress = branchAddress;
        Status = manufactureStatus;
    }

    [JsonConstructor]
    public PhotoBoothBranchDTO(string branchName, string branchAddress, ManufactureStatus manufactureStatus, Guid? accountID) {
        BranchName = branchName;
        BranchAddress = branchAddress;
        Status = manufactureStatus;
    }
}
