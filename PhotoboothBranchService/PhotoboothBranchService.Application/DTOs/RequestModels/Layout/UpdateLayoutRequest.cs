﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Layout
{
    public class UpdateLayoutRequest
    {
        public string LayoutURL { get; set; } = default!;
        public float LayoutPrice { get; set; } = default!;
    }
}
