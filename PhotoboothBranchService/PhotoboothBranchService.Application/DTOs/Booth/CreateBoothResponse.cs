using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class CreateBoothResponse
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; } = default!;
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
    }
}
