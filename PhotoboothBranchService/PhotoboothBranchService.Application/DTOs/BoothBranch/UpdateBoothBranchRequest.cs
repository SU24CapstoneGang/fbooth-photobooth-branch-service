using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.BoothBranch
{
    public class UpdateBoothBranchRequest
    {
        public string BranchName { get; set; } = default!;
        public string BranchAddress { get; set; } = default!;
        public ManufactureStatus Status { get; set; } = default!;
        public Guid AccountID { get; set; }
    }
}
