namespace PhotoboothBranchService.Domain.Entities
{
    public class PhotoSticker
    {
        public Guid PhotoStickerID { get; set; }
        public short Quantity { get; set; }
        public Guid? StickerID { get; set; }
        public virtual Sticker Sticker { get; set; }
        public Guid PhotoID { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
