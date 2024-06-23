using Microsoft.AspNetCore.Http;
using PhotoboothBranchService.Application.DTOs.Payment.MoMoPayment;
using PhotoboothBranchService.Application.DTOs.Payment.VNPayPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.PaymentServices.MoMoServices
{
    public interface IMoMoService
    {
        public string CreatePayment(MoMoRequest request);
        public Task HandlePaymentResponeIPN(MoMoResponse momoResponse);
        Task Return(IQueryCollection queryString);
    }
}
