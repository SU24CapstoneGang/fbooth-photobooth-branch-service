﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class SessionOrderFilter
    {
        public double? TotalPrice { get; set; } = default!;
        public DateTime? StartTime { get; set; } = default!;
        public DateTime? EndTime { get; set; } = default!;
        public BookingStatus? Status { get; set; }
        public Guid? BoothID { get; set; }
        public Guid? AccountID { get; set; }
    }
}
