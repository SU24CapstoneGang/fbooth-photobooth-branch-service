using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Booth
    {
        public Guid BoothID { get; set; }
        public string BoothName { get; set; }
        public ManufactureStatus Status { get; set; }
        public Guid PhotoBoothBranchID { get; set; }
        public virtual BoothBranch PhotoBoothBranch { get; set; }
        public virtual ICollection<PhotoSession> PhotoSessions { get; set; }
        public virtual ICollection<SessionOrder> SessionOrders { get; set; }
    }
}
