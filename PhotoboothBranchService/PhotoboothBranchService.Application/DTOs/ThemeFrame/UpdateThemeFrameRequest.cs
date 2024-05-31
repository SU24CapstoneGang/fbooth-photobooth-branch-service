﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.ThemeFrame
{
    public class UpdateThemeFrameRequest
    {
        public string ThemeFrameName { get; set; }
        public string ThemeFrameDescription { get; set; }
        public StatusUse Status { get; set; }
    }
}
