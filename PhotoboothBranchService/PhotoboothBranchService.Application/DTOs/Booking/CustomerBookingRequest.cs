using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Booking
{
    public class CustomerBookingRequest
    {
        public Guid BoothID { get; set; }
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
        public DateTime StartTime { get; set; } = default!;
        public DateTime EndTime { get; set; } = default!;
    }
}
