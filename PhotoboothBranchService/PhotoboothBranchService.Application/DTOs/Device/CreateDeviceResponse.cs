namespace PhotoboothBranchService.Application.DTOs.Device
{
    public class CreateDeviceResponse
    {
        public Guid DeviceID { get; set; }
        public string DeviceName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public Guid BoothID { get; set; }
    }
}
