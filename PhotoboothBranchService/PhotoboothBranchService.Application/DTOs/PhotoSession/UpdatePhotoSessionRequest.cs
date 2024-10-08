﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class UpdatePhotoSessionRequest
    {
        public string? SessionName { get; set; }
        public int? TotalPhotoTaken { get; set; }
        public PhotoSessionStatus? Status { get; set; }
    }
}
