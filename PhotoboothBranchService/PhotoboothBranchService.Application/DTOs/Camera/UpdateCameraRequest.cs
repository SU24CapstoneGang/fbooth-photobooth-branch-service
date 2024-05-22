namespace PhotoboothBranchService.Application.DTOs.Camera;

public class UpdateCameraRequest
{
    public string ModelName { get; set; }
    public string LensType { get; set; }
    public string Lens { get; set; }
    public float Price { get; set; }
    public Guid PhotoBoothBranchId { get; set; }
}
