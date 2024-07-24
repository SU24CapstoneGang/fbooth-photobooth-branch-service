﻿using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Device
{
    public class UpdateDeviceRequest
    {
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Device name must between 8 to 50 char characters")]
        public string DeviceName { get; set; } = default!;
    }
}
