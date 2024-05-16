namespace PhotoboothBranchService.Domain.Entities
{
    public class Filter
    {
        public Guid FilterID { get; set; } = default!;
        public string FilterName { get; set; } = default!;
        public string FilterURL { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public virtual List<EffectsPackLog> EffectsPackLogs { get; set; }
    }
}
