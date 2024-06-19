using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Frame
    {
        public Guid FrameID { get; set; }
        public string FrameName { get; set; } = default!;
        public string FrameURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public int Lenght { get; set; }
        public int Width { get; set; }
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid ThemeID { get; set; }
        public virtual Theme Theme { get; set; } = default!;
        public virtual ICollection<Photo> Photos { get; set; } = default!;
    }
}
