using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class PhotoSession
    {
        public Guid PhotoSessionID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; }
        public Guid SessionOrderID { get; set; }
        public SessionOrder SessionOrder { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<ServiceItem> ServiceItems { get; set; }
    }
}
