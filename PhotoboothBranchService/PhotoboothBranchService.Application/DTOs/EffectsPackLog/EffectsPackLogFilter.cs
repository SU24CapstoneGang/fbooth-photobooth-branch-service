namespace PhotoboothBranchService.Application.DTOs.EffectsPackLog
{
    public class EffectsPackLogFilter
    {
        public DateTime? CreateDate { get; set; }
        public Guid? PictureID { get; set; }
        public Guid? FrameID { get; set; }
        public Guid? FilterID { get; set; }
    }
}
