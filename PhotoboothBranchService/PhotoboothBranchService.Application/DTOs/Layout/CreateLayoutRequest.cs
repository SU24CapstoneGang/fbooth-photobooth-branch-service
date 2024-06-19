namespace PhotoboothBranchService.Application.DTOs.Layout
{
    public class CreateLayoutRequest
    {
        public string LayoutURL { get; set; } = default!;
        public float LayoutPrice { get; set; } = default!;
        public int Lenght { get; set; }
        public int Width { get; set; }
        public short PhotoSlot { get; set; }
    }
}
