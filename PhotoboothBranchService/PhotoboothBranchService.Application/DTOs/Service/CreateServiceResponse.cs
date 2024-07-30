namespace PhotoboothBranchService.Application.DTOs.ServiceType
{
    public class CreateServiceResponse
    {
        public Guid ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; } = default!;
    }
}
