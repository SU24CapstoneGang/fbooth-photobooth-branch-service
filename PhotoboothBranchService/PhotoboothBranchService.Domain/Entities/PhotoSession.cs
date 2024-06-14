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
        public int SessionIndex { get; set; }
        public int TotalPhotoTaken { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid LayoutID { get; set; }
        public virtual Layout Layout { get; set; } = default!;
        public Guid SessionOrderID { get; set; }
        public virtual SessionOrder SessionOrder { get; set; } = default!;
        public virtual ICollection<Photo> Photos { get; set; } = default!;
        public virtual ICollection<ServiceItem> ServiceItems { get; set; } = default!;
    }
}
