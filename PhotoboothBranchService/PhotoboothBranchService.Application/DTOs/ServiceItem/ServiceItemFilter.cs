using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class ServiceItemFilter
    {
        public short? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? Measure { get; set; }
        public string? Unit { get; set; } = default!;
        public Guid? PhotoSessionID { get; set; }
        public Guid? ServiceID { get; set; }
        public Guid? SessionOrderID { get; set; }
    }
}
