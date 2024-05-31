namespace PhotoboothBranchService.Application.DTOs.EffectsPackLog
{
    public class EffectsPackLogResponse
    {
        public Guid PacklogID { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid PictureID { get; set; }
        public Guid FrameID { get; set; }
        public Guid FilterID { get; set; }
    }
}
