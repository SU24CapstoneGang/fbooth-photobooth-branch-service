using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Booth
{
    public class UpdateBoothRequest
    {
        [Required, StringLength(50, MinimumLength = 8, ErrorMessage = "Booth name must between 8 to 50 char characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Booth name must not having special characters.")]
        public string? BoothName { get; set; }
        [Required, StringLength(50, MinimumLength = 8, ErrorMessage = "Background color must between 8 to 50 char characters")]
        public string? BackgroundColor { get; set; } = default!;
        [Required, StringLength(50, MinimumLength = 8, ErrorMessage = "Concept color must between 8 to 50 char characters")]
        public string? Concept { get; set; } = default!;
        public short? PeopleInBooth { get; set; }
        public Guid? BranchID { get; set; }
    }
}
