using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class CustomerBookingSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        public Guid SessionPackageID { get; set; }
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
        public DateTime StartTime { get; set; }
    }
}
