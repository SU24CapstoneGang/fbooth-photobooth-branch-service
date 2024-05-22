using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PaymentMethodMapper : Profile
    {
        public PaymentMethodMapper() { 
            CreateMap<CreatePaymentMethodRequest,PaymentMethod>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePaymentMethodRequest, PaymentMethod>().ReverseMap().HandleNullProperty();
            CreateMap<PaymentMethod, PaymentMethodResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
