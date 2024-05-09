using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Api.DTO;

public class CamerasDTO 
{
    public string CameraId {  get; set; } 
    public string ModelName { get; set; }
    public string SensorType { get; set; }
    public string Lens { get; set; }
    public float Price { get; set; }

    public CamerasDTO(string id, string modelName, string sensorType, string lens, float price)
    {
        CameraId = id;
        ModelName = modelName;
        SensorType = sensorType;
        Lens = lens;
        Price = price;
    }
    [JsonConstructor]

    public CamerasDTO(string modelName, string sensorType, string lens, float price)
    {
        ModelName = modelName;
        SensorType = sensorType;
        Lens = lens;
        Price = price;
    }
}
