using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Printer
{
    public class UpdatePrinterRequest
    {
        public string ModelName { get; set; } = default!;
        public float Price { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchId { get; set; }
    }
}
