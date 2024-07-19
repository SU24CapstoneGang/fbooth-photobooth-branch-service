using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.DTOs.MoMoPayment
{
    public class MoMoRefundResponse
    {
        public Guid RequestId { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public string PartnerRefId { get; set; }
        public string Transid { get; set; }
        public long Amount { get; set; }

    }
}
