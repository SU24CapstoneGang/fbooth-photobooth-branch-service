using PhotoboothBranchService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class DeviceMap : Entity
{
    public virtual Cameras Camera {  get; }
    public virtual Printers Printer { get; }

    public DeviceMap() { }
}
