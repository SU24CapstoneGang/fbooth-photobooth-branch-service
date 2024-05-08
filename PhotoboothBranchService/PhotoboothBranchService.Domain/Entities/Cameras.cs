using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class Cameras : Entity
{
    public string ModelName { get; } = null;
    public string SensorType { get; } = null;
    public string Lens { get; } = null;
    public float Price { get; }
    public virtual PhotoBoothBranches PhotoBoothBranch { get; }

    public Cameras(Guid id, string modelName, string sensorType, string lens, float price) : base(id)
    {
        ModelName = modelName;
        SensorType = sensorType;
        Lens = lens;
        Price = price;
    }
    private Cameras() { 
    }
}
