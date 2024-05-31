using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.MapSticker;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class MapStickerMapper : Profile
    {
        public MapStickerMapper()
        {
            CreateMap<CreateMapStickerRequest, MapSticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateMapStickerRequest, MapSticker>().ReverseMap().HandleNullProperty();
            CreateMap<MapSticker, MapStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
