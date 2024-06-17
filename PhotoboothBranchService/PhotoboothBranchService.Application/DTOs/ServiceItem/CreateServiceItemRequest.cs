using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class CreateServiceItemRequest
    {
        public short Quantity { get; set; }
        public Guid? LayoutID { get; set; }
        public Guid? PhotoSessionID { get; set; }
        public Guid ServiceID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
