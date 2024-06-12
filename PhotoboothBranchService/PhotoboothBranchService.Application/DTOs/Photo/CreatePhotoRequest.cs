using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Photo
{
    public class CreatePhotoRequest
    {
        public string? PhotoURL { get; set; }
        public PhotoVersion Version { get; set; }
        public Guid PhotoSessionID { get; set; }
        public Guid FrameID { get; set; }
        public Guid FilterID { get; set; }
        public Guid LayoutID { get; set; }
    }
}
