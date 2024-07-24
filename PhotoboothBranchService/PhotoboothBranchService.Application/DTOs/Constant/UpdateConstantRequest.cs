using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Constant
{
    public class UpdateConstantRequest
    {
        public string? DisplayName { get; set; } = default!;
        public string? ConstantValue { get; set; } = default!;
        public string? Description { get; set; } = default!;
    }
}
