using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class StickerResponse
    {
        public Guid StickerId { get; set; }
        public string StickerCode { get; set; } = default!;
        public string StickerURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public int stickerHeight { get; set; }
        public int stickerWidth { get; set; }
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid StickerTypeID { get; set; }
    }
}
