using PhotoboothBranchService.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Domain.Entities;

public class Camera 
{
    public Guid CameraID { get; set; }
    public string ModelName { get; set; }
    public string LensType { get; set; }
    public string Lens { get; set; }
    public float Price { get; set; }
    public ManufactureStatus Status { get; set; }
    public Guid PhotoBoothBranchId { get; set; } 
    public virtual PhotoBoothBranch PhotoBoothBranch { get; set; }
}
