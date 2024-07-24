using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ServicePackage
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; } = default!;
        public string ServiceDescription { get; set; } = default!;
        public decimal Price { get; set; }
        public int Measure { get; set; }
        public StatusUse Status { get; set; }
        public Guid ServiceTypeID { get; set; }
        public ServiceType ServiceType { get; set; } = default!;
        public virtual ICollection<ServiceSession> ServiceItems { get; set; } = default!;
    }
}
