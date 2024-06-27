using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class UpdatePhotoSessionRequest
    {
        public Guid? LayoutID { get; set; }
        public PhotoSessionStatus? Status { get; set; }
    }
}
