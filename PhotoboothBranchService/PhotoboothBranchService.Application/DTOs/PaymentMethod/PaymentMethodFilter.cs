﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PaymentMethod
{
    public class PaymentMethodFilter
    {
        public string? PaymentMethodName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public PaymentMethodStatus? Status { get; set; }
    }
}
