using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Branch;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BranchMapper : Profile
    {
        public BranchMapper()
        {
            CreateMap<CreateBranchRequest, Branch>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateBranchRequest, Branch>()
     .ForMember(dest => dest.BranchName, opt => opt.MapFrom((src, dest) => src.BranchName ?? dest.BranchName))
     .ForMember(dest => dest.Address, opt => opt.MapFrom((src, dest) => src.Address ?? dest.Address))
     .ForMember(dest => dest.Town, opt => opt.MapFrom((src, dest) => src.Town ?? dest.Town))
     .ForMember(dest => dest.City, opt => opt.MapFrom((src, dest) => src.City ?? dest.City))
     .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status));
            CreateMap<Branch, BranchResponse>()
                .ReverseMap()
                .HandleNullProperty();
            CreateMap<Branch, CreateBranchResponse>().HandleNullProperty();
        }
    }
}
