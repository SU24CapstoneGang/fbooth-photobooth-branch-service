using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.BoothBranch
{
    public class CreateBoothBranchRequest
    {
        public string BranchName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Town { get; set; } = default!;
        public string City { get; set; } = default!;
        public ManufactureStatus Status { get; set; } = default!;
        public Guid ManagerID { get; set; }
    }
}
