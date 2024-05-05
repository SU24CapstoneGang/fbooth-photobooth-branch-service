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
    public ManufactureStatus Status { get; }
    public string Lens { get; } = null;
    public DeviceMap DeviceMap { get; }
    public Cameras(Guid id, string modelName, string sensorType, ManufactureStatus status, string lens)
    {
        ModelName = modelName;
        SensorType = sensorType;
        Status = status;
        Lens = lens;
    }
}
