﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class CreateStickerResponse
    {
        public Guid StickerID { get; set; } = default!;
        public string StickerCode { get; set; } = default!;
        public string StrickerURL { get; set; } = default!;
        public int stickerHeight { get; set; }
        public int stickerWidth { get; set; }
        public string CouldID { get; set; } = default!;
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
