using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class CreateBoothRequest
    {
        public string BoothName { get; set; }
        public string BackgroundColor { get; set; } = default!;
        public string Concept { get; set; } = default!;
        [Range(1, 10, ErrorMessage ="Range for people in booth is from 1 to 10 people")]
        public short PeopleInBooth { get; set; }
        public Guid BranchID { get; set; }
    }
}
