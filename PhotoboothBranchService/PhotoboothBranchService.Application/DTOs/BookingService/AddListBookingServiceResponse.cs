using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class AddListBookingServiceResponse
    {
        public Guid SessionOrderID { get; set; }
        public Guid BoothID { get; set; }
        public List<BookingServiceResponse> Items { get; set; } = new List<BookingServiceResponse>();
    }
}
