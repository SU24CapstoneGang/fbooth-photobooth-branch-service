using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class BoothResponse
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; }
        public decimal PricePerHour { get; set; }
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        public short PeopleInBooth { get; set; }
        public bool isBooked { get; set; }
        public BoothStatus Status { get; set; }
        public Guid BranchID { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
