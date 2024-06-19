using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Theme;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ThemeFrameMapper : Profile
    {
        public ThemeFrameMapper()
        {
            CreateMap<CreateThemeRequest, Theme>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeRequest, Theme>().ReverseMap().HandleNullProperty();
            CreateMap<Theme, ThemeResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
