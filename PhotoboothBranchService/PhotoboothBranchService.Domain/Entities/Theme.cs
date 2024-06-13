using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Theme
    {
        public Guid ThemeID { get; set; }
        public string ThemeName { get; set; } = default!;
        public string ThemeFrameDescription { get; set; } = default!;
        public virtual ICollection<Frame> Frames { get; set; } = default!;
    }
}
