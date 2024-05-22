namespace PhotoboothBranchService.Application.DTOs.Layout
{
    public class Layoutresponse
    {
        public Guid LayoutID { get; set; }
        public string LayoutURL { get; set; } = default!;
        public float LayoutPrice { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
