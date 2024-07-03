namespace PhotoboothBranchService.Application.DTOs.Device
{
    public class DeviceFilter
    {
        public string? DeviceName { get; set; } = default!;
        public Guid? BoothID { get; set; }
    }
}
