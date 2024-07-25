using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ServicePackage
    {
        public Guid ServicePackageID { get; set; }
        public string PackageName { get; set; } = default!;
        public string PackageDescription { get; set; } = default!;
        public decimal PackagePrice { get; set; }
        public int Measure { get; set; }
        public StatusUse Status { get; set; }
        public Guid ServiceID { get; set; }
        public virtual Service Service { get; set; } = default!;
        public virtual ICollection<BookingService> BookingServices { get; set; } = default!;
    }
}
