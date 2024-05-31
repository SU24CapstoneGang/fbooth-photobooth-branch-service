namespace PhotoboothBranchService.Application.DTOs.EffectsPackLog
{
    public class CreateEffectsPackLogRequest
    {
        public Guid PictureID { get; set; }
        public Guid FrameID { get; set; }
        public Guid FilterID { get; set; }
    }

}
