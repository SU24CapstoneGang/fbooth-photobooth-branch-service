using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Domain.Entities;

public class Camera 
{
    public Guid CameraID { get; set; } = default!;
    public string ModelName { get; set; } = default!;
    public string SensorType { get; set; } = default!;
    public string Lens { get; set; } = default!;
    public float Price { get; set; } = default!;
    public Guid PhotoBoothBranchId { get; set; } 
    public virtual PhotoBoothBranch PhotoBoothBranch { get; set; }
}
