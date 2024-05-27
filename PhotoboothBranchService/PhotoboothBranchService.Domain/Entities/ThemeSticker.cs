using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ThemeSticker
    {
        public Guid ThemeStickerID { get; set; }
        public string ThemeStickerName { get; set; } = default!;
        public string ThemeStickerDescription { get; set; } = default!;
        public StatusUse Status { get; set; }
        public virtual List<Sticker> Stickers { get; set; }
    }
}
