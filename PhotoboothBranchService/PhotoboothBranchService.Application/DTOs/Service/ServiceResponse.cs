using PhotoboothBranchService.Domain;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class ServiceResponse
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; } = default!;
        public string ServiceDescription { get; set; } = default!;
        public string Unit { get; set; } = default!;
        public decimal ServicePrice { get; set; }
        public ServiceType ServiceType { get; set; }
        public StatusUse Status { get; set; }
    }
}
