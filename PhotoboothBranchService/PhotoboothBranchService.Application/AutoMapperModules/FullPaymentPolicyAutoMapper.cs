using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.FullPaymentPolicy;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class FullPaymentPolicyAutoMapper : Profile
    {
        public FullPaymentPolicyAutoMapper() 
        {
            CreateMap<FullPaymentPolicy, FullPaymentPolicyResponse>().ReverseMap();
            CreateMap<CreatePolicyRequestModel, FullPaymentPolicy>().ReverseMap();
            CreateMap<UpdatePolicyRequestModel, FullPaymentPolicy>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
