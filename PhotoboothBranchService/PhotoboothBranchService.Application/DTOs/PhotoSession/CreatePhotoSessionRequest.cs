namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class CreatePhotoSessionRequest
    {
        public Guid LayoutID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
