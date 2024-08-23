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
    .ForMember(dest => dest.PolicyName, opt => opt.MapFrom((src, dest) => src.PolicyName ?? dest.PolicyName))
    .ForMember(dest => dest.PolicyDescription, opt => opt.MapFrom((src, dest) => src.PolicyDescription ?? dest.PolicyDescription))
    .ForMember(dest => dest.RefundDaysBefore, opt => opt.MapFrom((src, dest) => src.RefundDaysBefore.HasValue ? src.RefundDaysBefore.Value : dest.RefundDaysBefore))
    .ForMember(dest => dest.CheckInTimeLimit, opt => opt.MapFrom((src, dest) => src.CheckInTimeLimit.HasValue ? src.CheckInTimeLimit.Value : dest.CheckInTimeLimit))
    .ForMember(dest => dest.IsActive, opt => opt.MapFrom((src, dest) => src.IsActive.HasValue ? src.IsActive.Value : dest.IsActive))
    .ForMember(dest => dest.StartDate, opt => opt.MapFrom((src, dest) => src.StartDate.HasValue ? src.StartDate.Value : dest.StartDate))
    .ForMember(dest => dest.EndDate, opt => opt.MapFrom((src, dest) => src.EndDate.HasValue ? src.EndDate.Value : dest.EndDate))
    .ForMember(dest => dest.RefundPercent, opt => opt.Ignore()) // Assuming this needs a specific value set elsewhere
    .ForMember(dest => dest.IsDefaultPolicy, opt => opt.Ignore()) // Assuming this needs a specific value set elsewhere
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Assuming this needs to be set when creating a new record
    .ForMember(dest => dest.LastModified, opt => opt.Ignore()); // Assuming this needs to be updated separately
        }
    }
}
