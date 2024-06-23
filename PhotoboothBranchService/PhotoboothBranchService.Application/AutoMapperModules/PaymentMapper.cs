using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<CreatePaymentRequest, Payment>().HandleNullProperty();
            CreateMap<UpdatePaymentRequest, Payment>().HandleNullProperty();
            CreateMap<Payment, PaymentResponse>().HandleNullProperty();
            CreateMap<Payment, CreatePaymentResponse>().HandleNullProperty();
        }
    }
}
