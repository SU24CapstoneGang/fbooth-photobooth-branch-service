using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class AddListServiceItemRequest
    {
        public Guid BoothID { get; set; }
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
    }
}
