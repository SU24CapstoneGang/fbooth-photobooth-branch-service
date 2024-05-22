using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Discount;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class DiscountMapper : Profile
    {
        public DiscountMapper()
        {
            CreateMap<CreateDiscountRequest, Discount>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateDiscountRequest, Discount>().ReverseMap().HandleNullProperty();
            CreateMap<Discountresponse, Discount>().ReverseMap().HandleNullProperty();
        }
    }
}
