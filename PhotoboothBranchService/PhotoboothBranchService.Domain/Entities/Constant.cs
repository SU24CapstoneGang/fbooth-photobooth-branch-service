using PhotoboothBranchService.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Constant
    {
        public string ConstantKey { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public string ConstantValue { get; set; } = default!;
        public ConstantType ConstantType { get; set; }
        public string Description { get; set; } = default!;
        public bool CanUpdateValue { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
