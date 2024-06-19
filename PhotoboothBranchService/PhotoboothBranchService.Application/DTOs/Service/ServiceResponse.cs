using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Service
{
    public class ServiceResponse
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal Price { get; set; }
        public int Measure { get; set; }
        public string Unit { get; set; } = default!;
        public Guid ServiceTypeID { get; set; }
    }
}
