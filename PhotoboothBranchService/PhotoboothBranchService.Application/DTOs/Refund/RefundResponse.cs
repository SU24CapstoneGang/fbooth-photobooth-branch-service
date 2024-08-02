﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Refund
{
    public class RefundResponse
    {
        public Guid RefundID { get; set; }
        public string GatewayTransactionID { get; set; }
        public DateTime RefundDateTime { get; set; }
        public string Description { get; set; }
        public RefundStatus Status { get; set; }
        public Guid TransactionID { get; set; }
    }
}
