﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class BoothFilter
    {
        public string? BoothName { get; set; }
        public BoothStatus? Status { get; set; }
        public string? BackgroundColor { get; set; } = default!;
        public string? Concept { get; set; } = default!;
        public short? PeopleInBooth { get; set; }
        public Guid? BranchID { get; set; }
    }
}
