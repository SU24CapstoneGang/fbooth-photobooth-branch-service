using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class EffectsPackLog
    {
        public Guid PackID { get; set; }
        public DateTime CreateDate { get; set; }
        public StatusUse Status { get; set; }
        public Guid PictureID { get; set; }
        public virtual FinalPicture FinalPicture { get; set; }
        public virtual List<Sticker> Stickers { get; set; }
        public Guid FrameID { get; set; }
        public virtual Frame Frame { get; set; }
        public Guid FilterID { get; set; }
        public virtual Filter Filter { get; set; }
    }
}
