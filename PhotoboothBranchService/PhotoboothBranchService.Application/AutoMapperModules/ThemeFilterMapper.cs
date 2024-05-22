using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ThemeFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ThemeFilterMapper : Profile
    {
        public ThemeFilterMapper() { 
            CreateMap<CreateThemeFilterRequest,ThemeFilterMapper>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeFilterRequest, ThemeFilterMapper>().ReverseMap().HandleNullProperty();
            CreateMap<ThemeFilterMapper, ThemeFilterResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
