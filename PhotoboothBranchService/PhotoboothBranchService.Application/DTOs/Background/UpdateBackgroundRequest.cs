using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Background
{
    public class UpdateBackgroundRequest
    {
        public string BackgroundCode { get; set; } = default!;
        public string BackgroundURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public int Lenght { get; set; }
        public int Width { get; set; }
        public StatusUse Status { get; set; }
    }
}
