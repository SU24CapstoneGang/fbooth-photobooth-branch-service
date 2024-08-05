using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class UpdateBoothRequest
    {
        [StringLength(50, ErrorMessage = "Booth name must between 8 to 50 char characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Booth name must not having special characters.")]
        public string? BoothName { get; set; }
        [Required, Range(0, 1000000, ErrorMessage = "Price is from 0 to 1 000 000")]
        public decimal PricePerHour { get; set; }
        [StringLength(50, ErrorMessage = "Background color must between 8 to 50 char characters")]
        public string? BackgroundColor { get; set; } = default!;
        [StringLength(50, ErrorMessage = "Concept color must between 8 to 50 char characters")]
        public string? Concept { get; set; } = default!;
        public short? PeopleInBooth { get; set; }
    }
}
