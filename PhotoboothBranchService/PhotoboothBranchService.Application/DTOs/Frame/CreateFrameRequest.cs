namespace PhotoboothBranchService.Application.DTOs.Frame
{
    public class CreateFrameRequest
    {
        public string FrameName { get; set; } = default!;
        public string FrameURL { get; set; } = default!;
    }
}
