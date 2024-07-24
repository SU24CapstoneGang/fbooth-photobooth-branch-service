using System.ComponentModel.DataAnnotations;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class CreateServiceItemRequest
    {
        [Range(1, 100, ErrorMessage = "Quantity must greater than 0")]
        public short Quantity { get; set; }
        public Guid ServiceID { get; set; }
        public Guid SessionOrderID { get; set; }
    }
}
