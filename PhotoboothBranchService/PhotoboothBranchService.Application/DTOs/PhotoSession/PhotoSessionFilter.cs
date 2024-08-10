using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class PhotoSessionFilter
    {
        public string? SessionName { get; set; }
        public Guid? PhotoSessionID { get; set; }
        public int? SessionIndex { get; set; }
        public int? TotalPhotoTaken { get; set; }
        public Guid? LayoutID { get; set; }
        public Guid? BookingID { get; set; }
        public PhotoSessionStatus? Status { get; set; }
    }
}
