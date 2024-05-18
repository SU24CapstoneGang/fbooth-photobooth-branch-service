using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs;

public class PhotoBoothBranchDTO
{
    public Guid? PhotoBoothBranchId { get; set; }
    public string BranchName { get; set; }
    public string BranchAddress { get; set; }
    public ManufactureStatus Status { get; set; }
    public Guid AccountID { get; set; }

}
