namespace PhotoboothBranchService.Application.DTOs.Device
{
    public class DeviceResponse
    {
        public Guid DeviceID { get; set; }
        public string DeviceName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public Guid BoothID { get; set; }
    }
}
