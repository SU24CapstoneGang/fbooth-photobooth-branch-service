using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class UpdateServiceRequest
    {
        [Required, StringLength(50, ErrorMessage = "Service's mame must have max lenght is 50")]
        public string ServiceName { get; set; } = default!;
        [Required, StringLength(150, ErrorMessage = "Service's description must have max lenght is 150")]
        public string ServiceDescription { get; set; } = default!;
        public string Unit { get; set; } = default!;
        [Required, Range(0, 5000000, ErrorMessage = "Price is from 0 to 5 000 000")]
        public decimal ServicePrice { get; set; }
        [Required]
        public StatusUse Status { get; set; }
    }
}
