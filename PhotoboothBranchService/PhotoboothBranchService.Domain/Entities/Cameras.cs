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
    public string ModelName { get; } = null;
    public string SensorType { get; } = null;
    public string Lens { get; } = null;
    public float Price { get; }
    public Guid? PhotoBoothBranchId {  get; } 
    public virtual PhotoBoothBranches PhotoBoothBranch { get; }

    [JsonConstructor]
    public Cameras(Guid id, string modelName, string sensorType, string lens, float price, Guid photoBoothBranchId) : base(id)
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
