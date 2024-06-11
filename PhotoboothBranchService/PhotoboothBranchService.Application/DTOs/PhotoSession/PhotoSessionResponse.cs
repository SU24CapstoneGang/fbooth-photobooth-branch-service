namespace PhotoboothBranchService.Application.DTOs.PhotoSession
{
    public class PhotoSessionResponse
    {
        public Guid PhotoSessionID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid BoothID { get; set; }
    }
}
