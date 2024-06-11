﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class UpdateBoothRequest
    {
        public string BoothName { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
    }
}
