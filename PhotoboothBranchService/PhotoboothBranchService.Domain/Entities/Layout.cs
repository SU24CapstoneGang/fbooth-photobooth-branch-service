using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Layout
    {
        public Guid LayoutID { get; set; }
        public string LayoutURL { get; set; } = default!;
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid FrameID { get; set; }
        public Frame Frame { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
