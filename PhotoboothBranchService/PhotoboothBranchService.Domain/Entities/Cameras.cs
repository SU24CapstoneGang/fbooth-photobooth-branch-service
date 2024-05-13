using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class Cameras : BaseEntity
{
    public string ModelName { get; set; } = null!;
    public string SensorType { get; set; } = null!;
    public string Lens { get; set; } = null!;
    public float Price { get; set; } 
    public Guid? PhotoBoothBranchId { get; set; } = null!;
    public virtual PhotoBoothBranches PhotoBoothBranch { get; set; } = null!;

    [JsonConstructor]
    public Cameras(Guid id, string modelName, string sensorType, string lens, float price, Guid? photoBoothBranchId) : base(id)
    {
        ModelName = modelName;
        SensorType = sensorType;
        Lens = lens;
        Price = price;
        PhotoBoothBranchId = photoBoothBranchId;
    }
    private Cameras() { 
    }
}
