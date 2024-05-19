using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ResponseModels.Layout
{
    public class Layoutresponse
    {
        public Guid LayoutID { get; set; }
        public string LayoutURL { get; set; } = default!;
        public float LayoutPrice { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
