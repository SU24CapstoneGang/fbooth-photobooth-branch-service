﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Layout
{
    public class CreateLayoutResponse
    {
        public Guid LayoutID { get; set; }
        public string LayoutURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public string LayoutCode { get; set; }
        public StatusUse Status { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public short PhotoSlot { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
