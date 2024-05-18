﻿using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class DiscountDTO
{
    public Guid? DiscountID { get; set; }
    public string DiscountCode { get; set; }
    public int RemaniningUsage { get; set; }
    public Decimal DiscountRate { get; set; }
    public DiscountStatus Status { get; set; }
}
