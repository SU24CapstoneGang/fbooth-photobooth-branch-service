using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class Printers : Entity
{
    public string ModelName { get; } = null;
    public ManufactureStatus Status { get; }
    public string Lens { get; } = null;
    public DeviceMap DeviceMap { get; }
    public Printers(Guid id, string modelName, ManufactureStatus status, string lens, DeviceMap deviceMap)
    {
        ModelName = modelName;
        Status = status;
        Lens = lens;
        DeviceMap = deviceMap;
    }
}
