using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class EffectsPackLog
    {
        public Guid PacklogID { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid PictureID { get; set; }
        public virtual FinalPicture FinalPicture { get; set; }
        public virtual List<MapSticker> MapStickers { get; set; }
        public Guid? FrameID { get; set; }
        public virtual Frame Frame { get; set; }
        public Guid? FilterID { get; set; }
        public virtual Filter Filter { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; }
    }
}
