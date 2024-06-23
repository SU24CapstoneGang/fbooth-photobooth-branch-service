namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class ServiceFilter
    {
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }
        public decimal Price { get; set; }
        public int Measure { get; set; }
        public string Unit { get; set; } = default!;
        public Guid? ServiceTypeID { get; set; }
    }
}
