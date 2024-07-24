using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Background
{
    public class CreateBackgroundRequest
    {
        public Guid LayoutID { get; set; }
        
        [Required, MaxLength(100, ErrorMessage ="Max length for this is 100")]
        public string BackgroundCode { get; set; } = default!;
        public int Height { get; set; }
        public int Width { get; set; }
        
    }
}
