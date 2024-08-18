using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.Common.Enum;
using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class UpdateServiceRequest
    {
        [StringLength(50, ErrorMessage = "Service's mame must have max lenght is 50")]
        public string? ServiceName { get; set; } = default!;
        [StringLength(150, ErrorMessage = "Service's description must have max lenght is 150")]
        public string? ServiceDescription { get; set; } = default!;
        [StringLength(50, ErrorMessage = "Service's unit must have max lenght is 50")]
        public string? Unit { get; set; } = default!;
        [Range(10000, 5000000, ErrorMessage = "Price is from 100000 to 5 000 000")]
        public decimal? ServicePrice { get; set; }
        public IFormFile? imgFile { get; set; }
        public StatusUse? Status { get; set; }
        public ServiceTypeForInput? ServiceType { get; set; }
    }
}
