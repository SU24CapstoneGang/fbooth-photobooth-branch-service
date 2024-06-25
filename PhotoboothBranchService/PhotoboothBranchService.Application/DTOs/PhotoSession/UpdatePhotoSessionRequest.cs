using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class UpdatePhotoSessionRequest
    {
        public int? TotalPhotoTaken { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? LayoutID { get; set; }
        public PhotoSessionStatus? Status { get; set; }
        public long? ValidateCode { get; set; }
    }
}
