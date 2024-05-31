﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.PrintPricing
{
    public class CreatePrintPricingRequest
    {
        public decimal DiscountPerPrintNumber { get; set; }
        public int MinQuantity { get; set; }
    }
}
