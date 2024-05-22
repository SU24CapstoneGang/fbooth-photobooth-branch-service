using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ThemeSticker;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ThemeStickerMapper : Profile
    {
        public ThemeStickerMapper() {
            CreateMap<CreateThemeStickerRequest,ThemeSticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeStickerRequest, ThemeSticker>().ReverseMap().HandleNullProperty();
            CreateMap<ThemeSticker,ThemeStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
