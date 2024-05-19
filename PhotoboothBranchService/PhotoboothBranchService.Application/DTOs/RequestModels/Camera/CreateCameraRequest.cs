namespace PhotoboothBranchService.Application.DTOs.RequestModels.Camera;

public class CreateCameraRequest
{
    public string ModelName { get; set; }
    public string SensorType { get; set; }
    public string Lens { get; set; }
    public float Price { get; set; }
    public Guid PhotoBoothBranchId { get; set; }
}
