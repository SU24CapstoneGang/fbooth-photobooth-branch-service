namespace PhotoboothBranchService.Domain.Entities
{
    public class ThemeFilter
    {
        public Guid ThemeFilterID { get; set; }
        public string ThemeFilterName { get; set; }
        public string ThemeFilterDescription { get; set; }
        public virtual List<Filter> Filters { get; set; }
    }
}
