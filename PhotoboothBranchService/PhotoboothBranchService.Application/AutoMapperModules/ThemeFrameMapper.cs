using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ThemeFrame;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ThemeFrameMapper : Profile
    {
        public ThemeFrameMapper()
        {
            CreateMap<CreateThemeFrameRequest, Theme>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeFrameRequest, Theme>().ReverseMap().HandleNullProperty();
            CreateMap<Theme, ThemeFrameResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
