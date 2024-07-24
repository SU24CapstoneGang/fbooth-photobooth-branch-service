using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Background
{
    public class UpdateBackgroundRequest
    {
        [MaxLength(100, ErrorMessage = "Max length for this is 100")]
        public string? BackgroundCode { get; set; } = default!;
        public string? BackgroundURL { get; set; } = default!;
        public string? CouldID { get; set; } = default!;
        public int? Height { get; set; }
        public int? Width { get; set; }
        public StatusUse? Status { get; set; }
    }
}
