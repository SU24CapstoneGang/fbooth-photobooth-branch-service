namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class CreatePhotoSessionRequest
    {
        public DateTime StartTime { get; set; }
        public Guid BoothID { get; set; }
    }
}
