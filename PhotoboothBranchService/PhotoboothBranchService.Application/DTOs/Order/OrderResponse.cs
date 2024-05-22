using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Order
{
    public class OrderResponse
    {
        public Guid OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int QuantityOfPicture { get; set; }
        public float TotalPrice { get; set; }
        public Guid SessionID { get; set; }
        public Guid PaymentMethodID { get; set; }
        public Guid DiscountID { get; set; }
        public Guid PictureID { get; set; }
        public Guid? AccountID { get; set; }
    }
}
