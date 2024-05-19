using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.ResponseModels.Printer
{
    public class PrinterResponse
    {
        public Guid PrinterID { get; set; }
        public string ModelName { get; set; } = default!;
        public float Price { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchId { get; set; }
        public string PhotoBoothBranchName { get; set; } = default!;
    }
}
