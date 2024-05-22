namespace PhotoboothBranchService.Domain.Entities
{
    public class Frame
    {
        public Guid FrameID { get; set; }
        public string FrameName { get; set; }
        public string FrameURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual List<EffectsPackLog> EffectsPackLogs { get; set; }
        public Guid ThemeFrameID { get; set; }
        public virtual ThemeFrame ThemeFrame { get; set; }
    }
}
