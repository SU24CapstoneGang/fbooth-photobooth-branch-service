using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class UpdatePhotoSessionRequest
    {
        public string? SessionName { get; set; }
        public int? SessionIndex { get; set; }
        public int? TotalPhotoTaken { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? LayoutID { get; set; }
    }
}
