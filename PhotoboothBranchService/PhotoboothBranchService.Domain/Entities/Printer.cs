namespace PhotoboothBranchService.Domain.Entities;

public class Printer
{
    public Guid PrinterID { get; set; } = default!;
    public string ModelName { get; set; } = default!;
    public float Price { get; set; } = default!;
    public Guid PhotoBoothBranchId { get; set; }
    public virtual PhotoBoothBranch PhotoBoothBranch { get; set; }
}
