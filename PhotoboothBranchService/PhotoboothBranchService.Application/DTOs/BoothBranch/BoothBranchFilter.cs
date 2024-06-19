using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.BoothBranch
{
    public class BoothBranchFilter
    {
        public string? BranchName { get; set; } = default!;
        public string? BranchAddress { get; set; } = default!;
        public DateTime? CreateDate { get; set; }
        public ManufactureStatus? Status { get; set; } = default!;
        public Guid? ManagerID { get; set; }
    }
}
