using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class BranchFilter
    {
        public string? BranchName { get; set; } = default!;
        public string? Address { get; set; } = default!;
        public string? Town { get; set; } = default!;
        public string? City { get; set; } = default!;
        public BranchStatus? Status { get; set; } = default!;
    }
}
