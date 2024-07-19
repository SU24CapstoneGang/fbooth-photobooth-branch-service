using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.SessionPackage
{
    public class CreateSessionPackageRequest
    {
        public string SessionPackageName { get; set; } = default!;
        public string SessionPackageDescription { get; set; } = default!;
        [Range(50000, 2000000, ErrorMessage ="Range of Price is 50.000 to 2.000.000 VND" )]
        public decimal Price { get; set; }
        [Range(1, 100, ErrorMessage = "Range of Email can sent is 1 to 100")]
        public short EmailSendCount { get; set; }
        [Range(1, 100, ErrorMessage = "Range of print is 1 to 100")]
        public short PrintCount { get; set; }
        [Range(5, 900, ErrorMessage = "Can not have duration more than 15 hours and less then 5 minutes")]
        public short Duration { get; set; }
    }
}
