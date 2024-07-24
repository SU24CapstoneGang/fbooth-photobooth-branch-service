using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class CreateServiceRequest
    {
        [Required, StringLength(100, MinimumLength = 8, ErrorMessage = "Service name must between 8 to 100 char characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Service name must not having special characters.")]
        public string ServiceName { get; set; }
        [Required, StringLength(255, MinimumLength = 8, ErrorMessage = "Description must between 8 to 255 char characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Description must not having special characters.")]
        public string ServiceDescription { get; set; }
        [Range(50000, 5000000, ErrorMessage ="Price for service is from 50.000 to 5.000.000 VND.")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public int Measure { get; set; }
        [Required, StringLength(30, MinimumLength = 8, ErrorMessage = "Unit must between 8 to 30 char characters.")]
        public string Unit { get; set; } = default!;
        public Guid ServiceTypeID { get; set; }
    }
}
