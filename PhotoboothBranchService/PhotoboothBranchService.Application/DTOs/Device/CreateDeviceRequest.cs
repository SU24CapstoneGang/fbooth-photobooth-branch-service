namespace PhotoboothBranchService.Application.DTOs.Device
{
    public class CreateDeviceRequest
    {
        public string DeviceName { get; set; } = default!;
        public Guid BoothID { get; set; }
    }
}
