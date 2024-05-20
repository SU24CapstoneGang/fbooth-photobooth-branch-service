using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.RequestModels.Layout
{
    public class LayoutFilter
    {
        public string? LayoutURL { get; set; } = default!;
        public float? LayoutPrice { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
