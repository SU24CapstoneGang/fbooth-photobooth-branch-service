using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PaymentMethod;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PaymentMethodMapper : Profile
    {
        public PaymentMethodMapper()
        {
            CreateMap<CreatePaymentMethodRequest, PaymentMethod>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePaymentMethodRequest, PaymentMethod>().ReverseMap().HandleNullProperty();
            CreateMap<PaymentMethod, PaymentMethodResponse>().HandleNullProperty();
            CreateMap<PaymentMethod, CreatePaymentMethodResponse>().HandleNullProperty();
        }
    }
}
