namespace PhotoboothBranchService.Application.DTOs.Filter
{
    public class CreateFilterRequest
    {
        public string FilterName { get; set; } = default!;
        public string FilterURL { get; set; } = default!;
    }
}

