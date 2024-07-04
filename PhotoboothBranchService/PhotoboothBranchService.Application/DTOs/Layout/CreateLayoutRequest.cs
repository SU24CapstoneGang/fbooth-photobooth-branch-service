namespace PhotoboothBranchService.Application.DTOs.Layout
{
    public class CreateLayoutRequest
    {
        public string LayoutCode { get; set; } = default!;
        public int Height { get; set; }
        public int Width { get; set; }
        public short PhotoSlot { get; set; }
    }
}
