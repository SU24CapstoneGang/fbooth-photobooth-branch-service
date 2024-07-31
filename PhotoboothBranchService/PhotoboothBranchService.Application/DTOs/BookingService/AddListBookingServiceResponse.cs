using PhotoboothBranchService.Application.DTOs.BookingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.BookingService
{
    public class AddListBookingServiceResponse
    {
        public Guid BookingID { get; set; }
        public Guid BoothID { get; set; }
        public List<BookingServiceResponse> Items { get; set; } = new List<BookingServiceResponse>();
    }
}
