namespace PhotoboothBranchService.Application.DTOs.Frame
{
    public class FrameResponse
    {
        public Guid FrameID { get; set; }
        public string FrameName { get; set; } = default!;
        public string FrameURL { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
