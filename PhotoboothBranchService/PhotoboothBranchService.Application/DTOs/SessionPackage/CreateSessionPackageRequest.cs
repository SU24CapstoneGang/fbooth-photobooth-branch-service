using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.SessionPackage
{
    public class CreateSessionPackageRequest
    {
        public string SessionPackageName { get; set; } = default!;
        public string SessionPackageDescription { get; set; } = default!;
        public decimal Price { get; set; }
        public short EmailSendCount { get; set; }
        public short PrintCount { get; set; }
        [Range(5, 900, ErrorMessage = "Can not have duration more than 15 hours")]
        public short Duration { get; set; }
    }
}
