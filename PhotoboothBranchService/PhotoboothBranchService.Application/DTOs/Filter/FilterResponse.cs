using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.DTOs.Filter
{
    public class Filterresponse
    {
        public Guid FilterID { get; set; }
        public string FilterName { get; set; } = default!;
        public string FilterURL { get; set; } = default!;
        public StatusUse Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
