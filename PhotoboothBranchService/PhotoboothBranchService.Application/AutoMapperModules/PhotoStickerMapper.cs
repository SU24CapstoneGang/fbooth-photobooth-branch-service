using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PhotoSticker;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoStickerMapper : Profile
    {
        public PhotoStickerMapper()
        {
            CreateMap<CreatePhotoStickerRequest, PhotoSticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePhotoStickerRequest, PhotoSticker>().ReverseMap().HandleNullProperty();
            CreateMap<PhotoSticker, PhotoStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
