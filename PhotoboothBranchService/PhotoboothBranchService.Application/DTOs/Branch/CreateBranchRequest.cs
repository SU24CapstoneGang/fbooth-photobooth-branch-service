using PhotoboothBranchService.Application.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class CreateBranchRequest
    {
        [Required, StringLength(50, ErrorMessage = "Branch name has max length is 50")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Branch name must not having special characters.")]
        public string BranchName { get; set; } = default!;
        [Required, StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string Address { get; set; } = default!;
        [Required, StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string Town { get; set; } = default!;
        [Required, StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string City { get; set; } = default!;
        [Required]
        [TimeSpanValidation]
        public TimeSpan OpeningTime { get; set; } // Thêm giờ mở cửa
        [Required]
        [TimeSpanValidation]
        public TimeSpan ClosingTime { get; set; } // Thêm giờ đóng cửa
    }
}
