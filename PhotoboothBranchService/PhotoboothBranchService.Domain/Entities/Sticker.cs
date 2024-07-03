using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Sticker
    {
        public Guid StickerID { get; set; } = default!;
        public string StickerCode { get; set; } = default!;
        public string StickerURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public int stickerHeight { get; set; }
        public int stickerWidth { get; set; }
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual ICollection<PhotoSticker> PhotoSticker { get; set; } = default!;
    }
}
