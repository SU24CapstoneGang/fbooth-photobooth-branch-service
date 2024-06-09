using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Photo
    {
        public Guid PhotoID { get; set; }
        public string PhotoURL { get; set; } = default!;
        public PhotoVersion Version { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid PhotoSessionID { get; set; }
        public virtual PhotoSession PhotoSession { get; set; }
        public Guid FrameID { get; set; }
        public virtual Frame Frame { get; set; }
        public Guid FilterID { get; set; }
        public virtual Filter Filter { get; set; }
        public Guid LayoutID {  get; set; }
        public virtual Layout Layout { get; set; }
        public virtual ICollection<PhotoSticker> PhotoStickers { get; set; }
    }
}
