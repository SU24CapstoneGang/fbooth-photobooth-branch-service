using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Frame
{
    public class CreateFrameRequest
    {
        public string FrameName { get; set; } = default!;
        public string FrameURL { get; set; } = default!;
        public int Lenght { get; set; }
        public int Width { get; set; }
        public StatusUse Status { get; set; }
        public Guid ThemeID { get; set; }
    }
}
