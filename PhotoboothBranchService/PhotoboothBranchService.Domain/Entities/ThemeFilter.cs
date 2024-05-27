using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ThemeFilter
    {
        public Guid ThemeFilterID { get; set; }
        public string ThemeFilterName { get; set; } = default!;
        public string ThemeFilterDescription { get; set; } = default!;
        public StatusUse Status { get; set; }
        public virtual List<Filter> Filters { get; set; }
    }
}
