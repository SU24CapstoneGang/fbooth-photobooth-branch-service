﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class CreateBoothResponse
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
    }
}
