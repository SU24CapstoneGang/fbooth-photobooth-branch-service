using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ThemeFrame;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ThemeFrameMapper : Profile
    {
        public ThemeFrameMapper() {
            CreateMap<CreateThemeFrameRequest, ThemeFrame>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeFrameRequest, ThemeFrame>().ReverseMap().HandleNullProperty();
            CreateMap<ThemeFrame, ThemeFrameResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
