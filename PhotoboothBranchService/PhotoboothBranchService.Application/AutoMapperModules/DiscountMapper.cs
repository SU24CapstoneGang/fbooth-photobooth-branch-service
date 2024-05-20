using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.RequestModels.Discount;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Discount;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
