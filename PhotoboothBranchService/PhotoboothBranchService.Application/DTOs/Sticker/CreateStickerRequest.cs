﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class CreateStickerRequest
    {
        public string StickerCode { get; set; } = default!;
        public int stickerHeight { get; set; }
        public int stickerWidth { get; set; }
        public StatusUse Status { get; set; }
        public Guid StickerTypeID { get; set; }
    }
}
