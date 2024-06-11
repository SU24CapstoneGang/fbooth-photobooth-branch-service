using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class UpdateServiceRequest
    {
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public double UnitPrice { get; set; }
        public Guid ServiceTypeID { get; set; }
    }
}
