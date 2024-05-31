using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.ThemeSticker
{
    public class UpdateThemeStickerRequest
    {
        public string ThemeStickerName { get; set; }
        public string ThemeStickerDescription { get; set; }
        public StatusUse Status { get; set; }
    }
}
