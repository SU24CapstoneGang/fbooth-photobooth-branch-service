using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<CreatePaymentRequest, Payment>().HandleNullProperty();
            CreateMap<UpdatePaymentRequest, Payment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Payment, PaymentResponse>().HandleNullProperty();
        }
    }
}
