using PhotoboothBranchService.Domain.Common;
using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities;

public class Printers : BaseEntity
{
    public string ModelName { get; } = null!;
    public float Price { get; }
    public Guid PhotoBoothBranchId { get; }
    public virtual PhotoBoothBranches PhotoBoothBranch { get; }
    [JsonConstructor]
    public Printers(Guid id, string modelName, string lens, float price, Guid photoBoothBranchId) : base(id)
    {
        ModelName = modelName;
        Price = price;
        PhotoBoothBranchId = photoBoothBranchId;
    }
    private Printers() { }
}
