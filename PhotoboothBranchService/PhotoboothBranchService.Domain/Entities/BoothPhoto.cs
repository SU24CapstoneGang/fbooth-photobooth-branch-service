using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Entities
{
    public class BoothPhoto
    {
        public Guid BoothPhotoId { get; set; }
        public string BoothPhotoUrl { get; set; } = default!;
        public string CouldID { get; set; } = default!;
        public Guid BoothID { get; set; }
        public virtual Booth Booth { get; set; } = default!;
    }
}
