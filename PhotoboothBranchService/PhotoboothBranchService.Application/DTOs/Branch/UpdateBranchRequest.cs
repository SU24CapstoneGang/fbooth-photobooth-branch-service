using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.BoothBranch
{
    public class UpdateBranchRequest
    {
        public string? BranchName { get; set; } = default!;
        public string? Address { get; set; } = default!;
        public string? Town { get; set; } = default!;
        public string? City { get; set; } = default!;
        public BranchStatus? BranchStatus { get; set; }
        public Guid? ManagerID { get; set; }
    }
}
