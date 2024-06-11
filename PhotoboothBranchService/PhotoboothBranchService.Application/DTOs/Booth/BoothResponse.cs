using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class BoothResponse
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
    }
}
