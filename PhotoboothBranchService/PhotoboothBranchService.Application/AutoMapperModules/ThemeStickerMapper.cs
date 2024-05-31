using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.ThemeSticker;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class ThemeStickerMapper : Profile
    {
        public ThemeStickerMapper()
        {
            CreateMap<CreateThemeStickerRequest, ThemeSticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateThemeStickerRequest, ThemeSticker>().ReverseMap().HandleNullProperty();
            CreateMap<ThemeSticker, ThemeStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
