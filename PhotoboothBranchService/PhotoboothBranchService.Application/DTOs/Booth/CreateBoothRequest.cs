﻿using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class CreateBoothRequest
    {
        [Required, StringLength(50 , ErrorMessage = "Booth name has max length is 50.")]
        public string BoothName { get; set; }
        [Required, StringLength(50, ErrorMessage = "Background color has max length is 50.")]
        public string BackgroundColor { get; set; } = default!;
        [Required, StringLength(50, ErrorMessage = "Concept color has max length is 50.")]
        public string Concept { get; set; } = default!;
        [Range(1, 10, ErrorMessage ="Range for people in booth is from 1 to 10 people.")]
        public short PeopleInBooth { get; set; }
        [Required]
        public long PricePerSlot { get; set; }
        [Required]
        public Guid BranchID { get; set; }

    }
}
