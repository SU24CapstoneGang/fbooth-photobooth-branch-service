using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class UpdateBranchRequest
    {
        [StringLength(50, ErrorMessage = "Branch name has max length is 50")]
        public string? BranchName { get; set; } = default!;
        [StringLength(100, ErrorMessage = "Branch address must between 8 to 100 char characters")]
        public string? Address { get; set; } = default!;
        [StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string? Town { get; set; } = default!;
        [StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string? City { get; set; } = default!;
        public BranchStatus? Status { get; set; } = default!;

    }
}
