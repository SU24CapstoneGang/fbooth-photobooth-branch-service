using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Layout
    {
        public Guid LayoutID { get; set; }
        public string LayoutCode { get; set; }
        public string LayoutURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public StatusUse Status { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public short PhotoSlot { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual ICollection<PhotoBox> PhotoBoxes { get; set; } = default!;
        public virtual ICollection<Background> Backgrounds { get; set; } = default!;
        public virtual ICollection<PhotoSession> PhotoSessions { get; set; } = default!;
    }
}
