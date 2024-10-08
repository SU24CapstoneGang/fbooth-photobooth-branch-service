﻿using PhotoboothBranchService.Domain;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class ServiceFilter
    {
        public string? ServiceName { get; set; } = default!;
        public string? ServiceDescription { get; set; } = default!;
        public string? Unit { get; set; } = default!;
        public decimal? ServicePrice { get; set; }
        public StatusUse? Status { get; set; }
        public ServiceType? ServiceType { get; set; }
    }
}
