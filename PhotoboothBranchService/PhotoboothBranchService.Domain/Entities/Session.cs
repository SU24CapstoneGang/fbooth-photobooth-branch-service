namespace PhotoboothBranchService.Domain.Entities
{
    public class Session
    {
        public Guid SessionID { get; set; } = default!;
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
        public Guid BranchesID { get; set; }
        public virtual PhotoBoothBranch PhotoBoothBranch { get; set; }
        public virtual Order Order { get; set; }
    }
}
