﻿using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Sticker
    {
        public Guid StickerID { get; set; } = default!;
        public string StickerName { get; set; } = default!;
        public string StrickerURL { get; set; } = default!;
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual ICollection<PhotoSticker> PhotoSticker { get; set; }
    }
}
