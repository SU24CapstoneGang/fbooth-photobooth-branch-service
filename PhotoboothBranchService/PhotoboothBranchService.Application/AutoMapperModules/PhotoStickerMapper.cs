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
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PhotoSticker, PhotoStickerResponse>().ReverseMap().HandleNullProperty();
            CreateMap<PhotoSticker, CreatePhotoStickerResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
