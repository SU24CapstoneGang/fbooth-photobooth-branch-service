using PhotoboothBranchService.Domain.Enum;
using System.Collections;

namespace PhotoboothBranchService.Domain.Entities
{
    public class Filter
    {
        public Guid FilterID { get; set; }
        public string FilterName { get; set; }
        public string FilterURL { get; set; }
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
