namespace PhotoboothBranchService.Domain.Entities
{
    public class Layout
    {
        public Guid LayoutID { get; set; }
        public string LayoutURL { get; set; } = default!;
        public double LayoutPrice { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public virtual List<EffectsPack> EffectsPacks { get; set; }
    }
}
