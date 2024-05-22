namespace PhotoboothBranchService.Application.DTOs.Layout
{
    public class UpdateLayoutRequest
    {
        public string LayoutURL { get; set; } = default!;
        public float LayoutPrice { get; set; } = default!;
    }
}
