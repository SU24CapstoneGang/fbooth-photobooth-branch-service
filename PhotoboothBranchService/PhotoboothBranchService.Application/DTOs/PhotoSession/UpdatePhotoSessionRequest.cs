namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class UpdatePhotoSessionRequest
    {
        public DateTime? EndTime { get; set; }
        public Guid BoothID { get; set; }
    }
}
