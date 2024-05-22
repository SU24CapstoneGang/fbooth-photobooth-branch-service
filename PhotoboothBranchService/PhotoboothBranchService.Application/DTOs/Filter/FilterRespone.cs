namespace PhotoboothBranchService.Application.DTOs.Filter
{
    public class Filterresponse
    {
        public Guid FilterID { get; set; }
        public string FilterName { get; set; } = default!;
        public string FilterURL { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
