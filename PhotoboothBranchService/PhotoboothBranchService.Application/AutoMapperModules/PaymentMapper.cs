using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper() 
        {
            CreateMap<CreatePaymentRequest, Payment>().HandleNullProperty();
            CreateMap<UpdatePaymentRequest, Payment>().HandleNullProperty();
            CreateMap<Payment, PaymentResponse>().HandleNullProperty();
        }
    }
}
