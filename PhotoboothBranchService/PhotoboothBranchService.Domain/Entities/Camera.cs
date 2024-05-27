using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class Camera
{
    public Guid CameraID { get; set; }
    public string ModelName { get; set; } = default!;
    public string LensType { get; set; } = default!;
    public float Price { get; set; }
    public ManufactureStatus Status { get; set; }
    public virtual PhotoBoothBranch PhotoBoothBranch { get; set; }
}
