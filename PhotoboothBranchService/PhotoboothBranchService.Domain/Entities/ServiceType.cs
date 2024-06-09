using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class ServiceType
    {
        public Guid ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set;}
        public virtual ICollection<Service> Services { get; set; }
    }
}
