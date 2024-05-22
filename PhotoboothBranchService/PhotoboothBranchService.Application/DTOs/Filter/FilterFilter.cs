using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Filter
{
    public class FilterFilter
    {
        public string? FilterName { get; set; }
        public DateTime? CreatedDateFrom { get; set; }
        public StatusUse? Status { get; set; }
        public DateTime? CreatedDateTo { get; set; }
        public DateTime? LastModifiedFrom { get; set; }
        public DateTime? LastModifiedTo { get; set; }
    }
}
