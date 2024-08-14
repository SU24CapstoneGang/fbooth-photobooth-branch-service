namespace PhotoboothBranchService.Domain.Entities
{
    public class Device
    {
        public Guid DeviceID { get; set; }
        public string DeviceName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; } = default!;
    }
}
