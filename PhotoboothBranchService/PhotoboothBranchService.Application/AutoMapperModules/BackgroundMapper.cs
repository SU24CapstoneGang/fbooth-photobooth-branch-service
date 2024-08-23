using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Background;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class BackgroundMapper : Profile
    {
        public BackgroundMapper()
        {
            CreateMap<CreateBackgroundRequest, Background>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateBackgroundRequest, Background>()
    .ForMember(dest => dest.BackgroundCode, opt => opt.MapFrom((src, dest) => src.BackgroundCode ?? dest.BackgroundCode))
    .ForMember(dest => dest.BackgroundURL, opt => opt.MapFrom((src, dest) => src.BackgroundURL ?? dest.BackgroundURL))
    .ForMember(dest => dest.CouldID, opt => opt.MapFrom((src, dest) => src.CouldID ?? dest.CouldID))
    .ForMember(dest => dest.Height, opt => opt.MapFrom((src, dest) => src.Height.HasValue ? src.Height.Value : dest.Height))
    .ForMember(dest => dest.Width, opt => opt.MapFrom((src, dest) => src.Width.HasValue ? src.Width.Value : dest.Width))
    .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status));
            CreateMap<BackgroundResponse, Background>().ReverseMap().HandleNullProperty();
            CreateMap<Background, CreateBackgroundResponse>().HandleNullProperty();
        }
    }
}
