namespace PhotoboothBranchService.Domain.Entities
{
    public class Service
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; } = default!;
        public string ServiceDescription { get; set; } = default!;
        public decimal Price { get; set; }
        public int Measure { get; set; }
        public string Unit { get; set; } = default!;
        public Guid ServiceTypeID { get; set; }
        public ServiceType ServiceType { get; set; } = default!;
        public virtual ICollection<ServiceItem> ServiceItems { get; set; } = default!;
    }
}
