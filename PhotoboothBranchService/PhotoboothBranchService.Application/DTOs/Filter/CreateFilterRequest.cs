using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Filter
{
    public class CreateFilterRequest
    {
        public string FilterName { get; set; } = default!;
        public string FilterURL { get; set; } = default!;
    }
}

