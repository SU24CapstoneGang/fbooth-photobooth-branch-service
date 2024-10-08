﻿using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class UpdateBoothRequest
    {
        [StringLength(50, ErrorMessage = "Booth name has max length is 50")]
        public string? BoothName { get; set; }
        [StringLength(50, ErrorMessage = "Background color has max length is 50")]
        public string? BackgroundColor { get; set; } = default!;
        [StringLength(50, ErrorMessage = "Concept color has max length is 50")]
        public string? Concept { get; set; } = default!;
        public short? PeopleInBooth { get; set; }
        public BoothStatus? Status { get; set; }
    }
}
