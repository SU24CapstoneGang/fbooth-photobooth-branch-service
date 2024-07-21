using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ServiceType
    {
        public Guid ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; } = default!;
        public StatusUse Status {  get; set; }
        public virtual ICollection<Service> Services { get; set; } = default!;
    }
}
