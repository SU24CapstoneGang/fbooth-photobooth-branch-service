using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.BoothBranch
{
    public class UpdateBranchRequest
    {
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Branch name must between 8 to 50 char characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Branch name must not having special characters.")]
        public string? BranchName { get; set; } = default!;
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Branch address must between 8 to 100 char characters")]
        public string? Address { get; set; } = default!;
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Branch address must between 8 to 100 char characters")]
        public string? Town { get; set; } = default!;
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Branch address must between 8 to 100 char characters")]
        public string? City { get; set; } = default!;
        public Guid? ManagerID { get; set; }
    }
}
