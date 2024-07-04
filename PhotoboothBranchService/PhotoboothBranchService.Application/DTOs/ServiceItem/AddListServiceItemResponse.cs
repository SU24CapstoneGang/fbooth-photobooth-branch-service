using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class AddListServiceItemResponse
    {
        public Guid SessionOrderID { get; set; }
        public Guid BoothID { get; set; }
        public List<ServiceItemResponse> Items { get; set; } = new List<ServiceItemResponse>();
    }
}
