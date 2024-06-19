using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class StickerFilter
    {
        public string? StickerName { get; set; }
        public StatusUse Status { get; set; }
    }
}
