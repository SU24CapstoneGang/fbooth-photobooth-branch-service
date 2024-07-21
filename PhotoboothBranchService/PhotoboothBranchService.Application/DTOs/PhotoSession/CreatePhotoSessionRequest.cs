namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class CreatePhotoSessionRequest
    {
        public Guid SessionOrderID { get; set; }
        public Guid LayoutID { get; set; }
    }
}
