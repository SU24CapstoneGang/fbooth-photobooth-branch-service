using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Background
{
    public class BackgroundFilter
    {
        public string? BackgroundCode { get; set; }
        public string? BackgroundURL { get; set; }
        public string? CouldID { get; set; } 
        public int? Height { get; set; }
        public int? Width { get; set; }
        public StatusUse? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid? LayoutID { get; set; }

    }
}
