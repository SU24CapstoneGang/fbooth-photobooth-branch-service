﻿namespace PhotoboothBranchService.Application.DTOs.PhotoSticker
{
    public class PhotoStickerFilter
    {
        public short Quantity { get; set; }
        public Guid? StickerID { get; set; }
        public Guid PhotoID { get; set; }
    }
}
