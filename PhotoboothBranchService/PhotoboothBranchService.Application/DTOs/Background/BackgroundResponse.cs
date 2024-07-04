using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Background
{
    public class BackgroundResponse
    {
        public Guid BackgroundID { get; set; }
        public string BackgroundCode { get; set; } = default!;
        public string BackgroundURL { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public int Height { get; set; }
        public int Width { get; set; }
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid LayoutID { get; set; }
    }
}
