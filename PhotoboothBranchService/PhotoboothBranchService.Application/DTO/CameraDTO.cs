using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTO;

public class CameraDTO 
{
    public Guid? CameraId {  get; set; } 
    public string ModelName { get; set; }
    public string SensorType { get; set; }
    public string Lens { get; set; }
    public float Price { get; set; }
    public Guid? PhotoBoothBranchId { get; set; }
    //contrustor respone
    public CameraDTO(Guid cameraId, string modelName, string sensorType, string lens, float price, Guid? photoBoothBranchId)
    {
        CameraId = cameraId;
        ModelName = modelName;
        SensorType = sensorType;
        Lens = lens;
        Price = price;
        PhotoBoothBranchId = photoBoothBranchId;
    }
    //contrustor request
    [JsonConstructor]
    public CameraDTO(string modelName, string sensorType, string lens, float price, Guid? photoBoothBranchId)
    {
        ModelName = modelName;
        SensorType = sensorType;
        Lens = lens;
        Price = price;
        PhotoBoothBranchId = photoBoothBranchId;
    }
    public CameraDTO() { }
}
