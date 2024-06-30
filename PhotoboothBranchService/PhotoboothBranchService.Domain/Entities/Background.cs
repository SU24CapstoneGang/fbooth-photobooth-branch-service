using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Background
    {
        public Guid BackgroundID { get; set; }
        public string BackgroundCode { get; set; } = default!;
        public string BackgroundURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public int Height { get; set; }
        public int Width { get; set; }
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; }
        public virtual ICollection<Photo> Photos { get; set; } = default!;
    }
}
