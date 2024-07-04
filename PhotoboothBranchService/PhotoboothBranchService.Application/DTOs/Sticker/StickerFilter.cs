using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class StickerFilter
    {
        public string? StickerCode { get; set; } = default!;
        public int? stickerHeight { get; set; }
        public int? stickerWidth { get; set; }
        public StatusUse? Status { get; set; }
    }
}
