using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Printer
{
    public class CreatePrinterRequest
    {
        public string ModelName { get; set; } = default!;
        public float Price { get; set; }
    }
}
