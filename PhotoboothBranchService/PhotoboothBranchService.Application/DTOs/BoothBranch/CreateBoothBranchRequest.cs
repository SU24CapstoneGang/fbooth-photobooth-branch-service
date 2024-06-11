using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.BoothBranch
{
    public class CreateBoothBranchRequest
    {
        public string BranchName { get; set; } = default!;
        public string BranchAddress { get; set; } = default!;
        public ManufactureStatus Status { get; set; } = default!;
        public Guid? AccountID { get; set; }
        public Guid? CameraID { get; set; }
        public Guid? PrinterID { get; set; }
    }
}
