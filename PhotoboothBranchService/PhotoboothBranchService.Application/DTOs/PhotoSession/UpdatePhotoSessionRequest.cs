namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class UpdatePhotoSessionRequest
    {
        public int SessionIndex { get; set; }
        public int TotalPhotoTaken { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid LayoutID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
