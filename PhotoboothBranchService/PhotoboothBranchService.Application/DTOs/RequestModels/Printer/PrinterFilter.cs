using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Printer
{
    public class PrinterFilter
    {
        public string? ModelName { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public ManufactureStatus? Status { get; set; }
        public Guid? PhotoBoothBranchId { get; set; }
    }
}
