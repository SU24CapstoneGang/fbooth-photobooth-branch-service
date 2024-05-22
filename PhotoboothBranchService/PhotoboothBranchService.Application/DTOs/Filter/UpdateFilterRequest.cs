using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Filter
{
    public class UpdateFilterRequest
    {
        public string FilterName { get; set; } = default!;
        public string FilterURL { get; set; } = default!;
        public StatusUse Status { get; set; }
    }
}

