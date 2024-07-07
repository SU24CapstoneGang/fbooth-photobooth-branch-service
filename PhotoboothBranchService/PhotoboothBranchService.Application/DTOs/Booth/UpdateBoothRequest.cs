using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class UpdateBoothRequest
    {
        public string BoothName { get; set; }
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
    }
}
