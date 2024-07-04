using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Background
{
    public class CreateBackgroundRequest
    {
        public Guid LayoutID { get; set; }
        public string BackgroundCode { get; set; } = default!;
        public int Height { get; set; }
        public int Width { get; set; }
        
    }
}
