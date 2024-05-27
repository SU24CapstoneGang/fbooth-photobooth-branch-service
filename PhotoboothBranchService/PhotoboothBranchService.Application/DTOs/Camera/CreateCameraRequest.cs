namespace PhotoboothBranchService.Application.DTOs.Camera;

public class CreateCameraRequest
{
    public string ModelName { get; set; }
    public string LensType { get; set; }
    public float Price { get; set; }
}
