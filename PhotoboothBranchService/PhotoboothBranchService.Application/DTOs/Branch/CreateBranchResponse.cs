﻿using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class CreateBranchResponse
    {
        public Guid BranchID { get; set; }
        public string BranchName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Town { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public BranchStatus Status { get; set; } = default!;
        public TimeSpan OpeningTime { get; set; } // Thêm giờ mở cửa
        public TimeSpan ClosingTime { get; set; } // Thêm giờ đóng cửa
    }
}
