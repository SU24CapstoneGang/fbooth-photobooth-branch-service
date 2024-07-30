using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Service
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; } = default!;
        public string ServiceDescription { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public decimal ServicePrice { get; set; }
        public StatusUse Status {  get; set; }
        public virtual ICollection<BookingService> BookingServices { get; set; }
    }
}
