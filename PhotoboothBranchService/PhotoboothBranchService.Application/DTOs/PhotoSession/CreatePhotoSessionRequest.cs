namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class CreatePhotoSessionRequest
    {
        public string SessionName { get; set; } = default!;
        public int TotalPhotoTaken { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid LayoutID { get; set; }
        public Guid BookingID { get; set; }
    }
}
