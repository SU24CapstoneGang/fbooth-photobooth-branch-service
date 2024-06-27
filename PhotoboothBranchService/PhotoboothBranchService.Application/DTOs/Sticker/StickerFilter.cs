using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Sticker
{
    public class StickerFilter
    {
        public string? StickerCode { get; set; } = default!;
        public StatusUse? Status { get; set; }
    }
}
