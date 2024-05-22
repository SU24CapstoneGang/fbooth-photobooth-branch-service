using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoBoothBranch
{
    public class UpdatePhotoBoothBranchRequest
    {
        public string BranchName { get; set; } = default!;
        public string BranchAddress { get; set; } = default!;
        public ManufactureStatus Status { get; set; } = default!;
        public Guid AccountID { get; set; }
    }
}
