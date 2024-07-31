namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class CreatePhotoSessionRequest
    {
        public Guid BookingID { get; set; }
        public Guid LayoutID { get; set; }
    }
}
