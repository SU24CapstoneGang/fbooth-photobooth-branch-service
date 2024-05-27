namespace PhotoboothBranchService.Domain.Entities
{
    public class MapSticker
    {
        public Guid MapStickerID { get; set; }
        public Guid PackLogID { get; set; }
        public virtual EffectsPackLog EffectsPackLog { get; set; }
        public Guid? StickerId { get; set; }
        public virtual Sticker Sticker { get; set; }
    }
}
