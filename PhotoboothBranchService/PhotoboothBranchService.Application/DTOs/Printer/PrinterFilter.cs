using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Printer
{
    public class PrinterFilter
    {
        public string? ModelName { get; set; }
        public float Price { get; set; } 
        public ManufactureStatus Status { get; set; }
    }
}
