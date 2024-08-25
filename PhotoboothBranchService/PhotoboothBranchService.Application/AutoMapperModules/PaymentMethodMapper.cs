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
            CreateMap<UpdatePaymentMethodRequest, PaymentMethod>()
     .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom((src, dest) => src.PaymentMethodName ?? dest.PaymentMethodName))
     .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status));
            CreateMap<PaymentMethod, PaymentMethodResponse>().HandleNullProperty();
            CreateMap<PaymentMethod, CreatePaymentMethodResponse>().HandleNullProperty();
        }
    }
}
