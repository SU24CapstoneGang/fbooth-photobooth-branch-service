using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ThemeFilter;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ThemeFilterMapper : Profile
    {
        public ThemeFilterMapper()
        {
            CreateMap<CreateThemeFilterRequest, ThemeFilterMapper>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeFilterRequest, ThemeFilterMapper>().ReverseMap().HandleNullProperty();
            CreateMap<ThemeFilterMapper, ThemeFilterResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
