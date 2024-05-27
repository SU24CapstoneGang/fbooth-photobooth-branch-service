using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ThemeFrame
    {
        public Guid ThemeFrameID { get; set; }
        public string ThemeFrameName { get; set; } = default!;
        public string ThemeFrameDescription { get; set; } = default!;
        public StatusUse Status { get; set; }
        public virtual List<Frame> Frames { get; set; }
    }
}
