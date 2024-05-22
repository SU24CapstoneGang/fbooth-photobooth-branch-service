namespace PhotoboothBranchService.Application.DTOs.Session
{
    public class SessionFilter
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? BranchesID { get; set; }
    }
}
