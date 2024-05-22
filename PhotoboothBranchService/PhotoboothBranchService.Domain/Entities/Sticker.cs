namespace PhotoboothBranchService.Domain.Entities
{
    public class Sticker
    {
        public Guid StickerId { get; set; } = default!;
        public string StickerName { get; set; } = default!;
        public string StrickerURL { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid PackID { get; set; }
        public virtual EffectsPackLog EffectsPackLog { get; set; }
    }
}
