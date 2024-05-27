using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class Printer
{
    public Guid PrinterID { get; set; } 
    public string ModelName { get; set; } = default!;
    public float Price { get; set; } = default!;
    public ManufactureStatus Status { get; set; }
    public virtual PhotoBoothBranch PhotoBoothBranch { get; set; }
}
