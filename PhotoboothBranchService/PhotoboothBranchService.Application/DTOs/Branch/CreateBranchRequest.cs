using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class CreateBranchRequest
    {
        [Required, StringLength(50, ErrorMessage = "Branch name has max length is 50")]
        public string BranchName { get; set; } = default!;
        [Required, StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string Address { get; set; } = default!;
        [Required, StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string Town { get; set; } = default!;
        [Required, StringLength(100, ErrorMessage = "Branch address has max length is 100")]
        public string City { get; set; } = default!;
        [Required]
        public BranchStatus Status { get; set; } = default!;
    }
}
