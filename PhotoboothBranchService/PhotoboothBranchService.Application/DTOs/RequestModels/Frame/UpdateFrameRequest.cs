﻿namespace PhotoboothBranchService.Application.DTOs.RequestModels.Frame
{
    public class UpdateFrameRequest
    {
        public string FrameName { get; set; } = default!;
        public string FrameURL { get; set; } = default!;
    }
}
