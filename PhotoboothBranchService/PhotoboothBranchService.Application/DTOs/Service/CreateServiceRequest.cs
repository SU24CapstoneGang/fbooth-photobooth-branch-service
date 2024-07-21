using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class CreateServiceRequest
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        [Range(50000, 5000000, ErrorMessage ="Price for service is from 50.000 to 5.000.000 VND")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0.")]
        public int Measure { get; set; }
        public string Unit { get; set; } = default!;
        public Guid ServiceTypeID { get; set; }
    }
}
