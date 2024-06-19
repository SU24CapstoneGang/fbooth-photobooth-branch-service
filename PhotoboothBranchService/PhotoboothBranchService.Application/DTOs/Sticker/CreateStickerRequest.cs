﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class CreateStickerRequest
    {
        public string StickerName { get; set; } = default!;
        public string StickerURL { get; set; } = default!;
        public StatusUse Status { get; set; }
    }
}
