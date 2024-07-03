﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class SessionOrderResponse
    {
        public Guid SessionOrderID { get; set; }
        public decimal TotalPrice { get; set; } = default!;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public SessionOrderStatus Status { get; set; }
        public Guid AccountID { get; set; }
        //  public List<ServiceItemResponse> ServiceItems { get; set; } = new List<ServiceItemResponse>();
    }
}
