using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class EffectsPack 
    {
        public Guid PackID { get; set; }
        public DateTime CreateDate { get; set; } = default!;
        public StatusUse Status { get; set; } = default!;
        public float PackagePrice { get; set; } = default!;
        public virtual FinalPicture FinalPicture { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; }
        public Guid StickerID { get; set; }
        public virtual Sticker Sticker { get; set; }
        public Guid FrameID { get; set; }
        public virtual Frame Frame { get; set; }
        public Guid FilterID { get; set; }
        public virtual Filter Filter { get; set; }
    }
}
