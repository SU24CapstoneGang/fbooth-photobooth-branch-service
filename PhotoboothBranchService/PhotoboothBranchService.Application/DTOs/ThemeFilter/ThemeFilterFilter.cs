using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.ThemeFilter
{
    public class ThemeFilterFilter
    {
        public string? ThemeFilterName { get; set; }
        public string? ThemeFilterDescription { get; set; }
        public StatusUse? Status { get; set; }
    }
}
