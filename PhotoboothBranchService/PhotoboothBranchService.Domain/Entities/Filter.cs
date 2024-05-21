namespace PhotoboothBranchService.Domain.Entities
{
    public class Filter
    {
        public Guid FilterID { get; set; }
        public string FilterName { get; set; }
        public string FilterURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public virtual List<EffectsPackLog> EffectsPackLogs { get; set; }
        public Guid ThemeFilterID { get; set; }
        public virtual ThemeFilter ThemeFilter { get; set; }
    }
}
