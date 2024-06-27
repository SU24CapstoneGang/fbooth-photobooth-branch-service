using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.SessionOrder
{
    public class ValidateSessionOrderRequest
    {
        public Guid BoothID { get; set; }
        public long ValidateCode { get; set; }
    }
}
