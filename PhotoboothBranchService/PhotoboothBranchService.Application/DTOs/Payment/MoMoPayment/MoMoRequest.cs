using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.Payment.MoMoPayment
{
    public class MoMoRequest
    {
        public string orderInfo { get; set; }
        public long amount { get; set; }
        public string orderId { get; set; } //payment ID in db
        public string requestId { get; set; } 
        public string extraData { get; set; }
    }
}
