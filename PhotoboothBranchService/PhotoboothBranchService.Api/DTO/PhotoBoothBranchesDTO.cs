using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.DTO;

public class PhotoBoothBranchesDTO
{
    public string PhotoBoothBranchId { get; set; }
    public string BranchName { get; } = null;
    public string BranchAddress { get; } = null;
    public ManufactureStatus Status { get; }
    public Accounts Account { get; }

    public PhotoBoothBranchesDTO(string photoBoothBranchId, string branchName, string branchAddress, ManufactureStatus manufactureStatus)
    {
        PhotoBoothBranchId = photoBoothBranchId;
        BranchName = branchName;
        BranchAddress = branchAddress;
        Status = manufactureStatus;
    }
    [JsonConstructor]

    public PhotoBoothBranchesDTO(string branchName, string branchAddress, ManufactureStatus manufactureStatus) {
        BranchName = branchName;
        BranchAddress = branchAddress;
        Status = manufactureStatus;
    }
}
