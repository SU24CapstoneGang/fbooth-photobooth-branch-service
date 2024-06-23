namespace PhotoboothBranchService.Application.DTOs.ServiceType
{
    public class CreateServiceTypeResponse
    {
        public Guid ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; } = default!;
    }
}
