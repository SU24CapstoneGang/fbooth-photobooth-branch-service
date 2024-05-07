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
    public float Price { get; }
    public virtual PhotoBoothBranches PhotoBoothBranch { get; }
    public Printers(Guid id, string modelName, string lens, float price)
    {
        ModelName = modelName;
        Price = price;
    }
    private Printers() { }
}
