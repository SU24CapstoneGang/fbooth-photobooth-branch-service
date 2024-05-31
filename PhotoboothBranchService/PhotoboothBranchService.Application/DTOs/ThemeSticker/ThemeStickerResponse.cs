using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.ThemeSticker
{
    public class ThemeStickerResponse
    {
        public Guid ThemeStickerID { get; set; }
        public string ThemeStickerName { get; set; }
        public string ThemeStickerDescription { get; set; }
        public StatusUse Status { get; set; }
    }
}
