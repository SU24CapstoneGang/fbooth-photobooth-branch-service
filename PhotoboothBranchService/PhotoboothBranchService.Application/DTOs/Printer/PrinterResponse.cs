using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Printer
{
    public class PrinterResponse
    {
        public Guid PrinterID { get; set; }
        public string ModelName { get; set; }
        public float Price { get; set; }
        public ManufactureStatus Status { get; set; }
    }
}
