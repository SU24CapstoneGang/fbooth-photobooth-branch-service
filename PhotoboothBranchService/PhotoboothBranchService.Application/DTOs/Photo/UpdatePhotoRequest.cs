using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Photo
{
    public class UpdatePhotoRequest
    {
        public string? PhotoURL { get; set; } = default!;
        public PhotoVersion? Version { get; set; }
        public string? CouldID { get; set; } = default!;
        public DateTime? CreateDate { get; set; }
        public Guid? PhotoSessionID { get; set; }
        public Guid? BackgroundID { get; set; }
    }
}
