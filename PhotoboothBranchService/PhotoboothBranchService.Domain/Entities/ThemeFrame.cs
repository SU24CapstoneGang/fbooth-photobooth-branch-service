using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ThemeFrame
    {
        public Guid ThemeFrameID { get; set; }
        public string ThemeFrameName { get; set; }
        public string ThemeFrameDescription { get; set; }
        public StatusUse Status { get; set; }
        public virtual List<Frame> Frames { get; set; }
    }
}
