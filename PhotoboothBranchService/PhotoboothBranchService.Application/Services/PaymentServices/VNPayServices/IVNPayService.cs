using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PaymentServices.VNPayServices
{
    public interface IVNPayService
    {
        public Task<string>Pay(PaymentRequest paymentRequest);
        public void Query();
        public void Refund();
        public void Return();
    }
}
