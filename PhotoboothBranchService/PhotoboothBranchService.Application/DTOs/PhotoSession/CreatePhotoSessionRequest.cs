namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class CreatePhotoSessionRequest
    {
        public string SessionName { get; set; } = default!;
        public Guid LayoutID { get; set; }
        public Guid BookingID { get; set; }
    }
}
