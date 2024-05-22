using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Camera;

public class Cameraresponse
{
    public Guid CameraID { get; set; }
    public string ModelName { get; set; }
    public string LensType { get; set; }
    public string Lens { get; set; }
    public float Price { get; set; }
    public ManufactureStatus Status { get; set; }
    public Guid PhotoBoothBranchId { get; set; }
    public string PhotoBoothBranchName { get; set; }
}
