namespace PhotoboothBranchService.Domain.Entities
{
    public class ThemeSticker
    {
        public Guid ThemeStickerID { get; set; }
        public string ThemeStickerName { get; set; }
        public string ThemeStickerDescription { get; set; }
        public virtual List<Sticker> Stickers { get; set; }
    }
}
