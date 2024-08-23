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
            CreateMap<UpdatePhotoStickerRequest, PhotoSticker>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom((src, dest) => src.Quantity.HasValue ? src.Quantity.Value : dest.Quantity));
            CreateMap<PhotoSticker, PhotoStickerResponse>().ReverseMap().HandleNullProperty();
            CreateMap<PhotoSticker, CreatePhotoStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
