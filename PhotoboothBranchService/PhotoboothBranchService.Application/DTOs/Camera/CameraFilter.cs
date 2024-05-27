using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Camera
{
    public class CameraFilter
    {
        public string? ModelName { get; set; }
        public string? LensType { get; set; }
        public float? Price { get; set; }
        public ManufactureStatus CameraStatus { get; set; }
    }
}
