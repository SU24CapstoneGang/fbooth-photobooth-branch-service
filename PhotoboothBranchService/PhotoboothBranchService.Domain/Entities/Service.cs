using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Service
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double UnitPrice { get; set; }
        public Guid ServiceTypeID { get; set; }
        public ServiceType ServiceType { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}
