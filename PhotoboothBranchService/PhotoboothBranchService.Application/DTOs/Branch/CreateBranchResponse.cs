using PhotoboothBranchService.Application.DTOs.Booth;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Branch
{
    public class CreateBranchResponse
    {
        public Guid BranchID { get; set; }
        public string BranchName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Town { get; set; } = default!;
        public string City { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public BranchStatus Status { get; set; } = default!;
        public Guid? ManagerID { get; set; }
    }
}
