using PhotoboothBranchService.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.ServiceItem
{
    public class AddListBookingServiceRequest
    {
        public Guid BoothID { get; set; }
        [DictionaryValueGreaterThanZero(ErrorMessage = "Each service quantity must be greater than 0.")]
        public Dictionary<Guid, short> ServiceList { get; set; } = new Dictionary<Guid, short>();
    }
}
