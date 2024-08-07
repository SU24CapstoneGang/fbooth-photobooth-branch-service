using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class CreateBoothRequest
    {
        [Required, StringLength(50 , ErrorMessage = "Booth name has max length is 50.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Botth name must not having special characters.")]
        public string BoothName { get; set; }
        [Required, Range(0, 1000000, ErrorMessage = "Price is from 0 to 1 000 000")]
        public decimal PricePerHour { get; set; }
        [Required, StringLength(50, ErrorMessage = "Background color has max length is 50.")]
        public string BackgroundColor { get; set; } = default!;
        [Required, StringLength(50, ErrorMessage = "Concept color has max length is 50.")]
        public string Concept { get; set; } = default!;
        [Range(1, 10, ErrorMessage ="Range for people in booth is from 1 to 10 people.")]
        public short PeopleInBooth { get; set; }
        public Guid BranchID { get; set; }
    }
}
