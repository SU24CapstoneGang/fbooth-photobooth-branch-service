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
            CreateMap<CreateThemeFrameRequest, ThemeFrame>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeFrameRequest, ThemeFrame>().ReverseMap().HandleNullProperty();
            CreateMap<ThemeFrame, ThemeFrameResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
