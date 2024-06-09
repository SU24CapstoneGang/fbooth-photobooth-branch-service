using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.MapSticker;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoStickerMapper : Profile
    {
        public PhotoStickerMapper()
        {
            CreateMap<CreateMapStickerRequest, PhotoSticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateMapStickerRequest, PhotoSticker>().ReverseMap().HandleNullProperty();
            CreateMap<PhotoSticker, MapStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
