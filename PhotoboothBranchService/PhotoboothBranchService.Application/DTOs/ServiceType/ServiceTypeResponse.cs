using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ServiceType
{
    public class ServiceTypeResponse
    {
        public Guid ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
    }
}
