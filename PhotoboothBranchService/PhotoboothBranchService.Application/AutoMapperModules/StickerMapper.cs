using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Sticker;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class StickerMapper : Profile
    {
        public StickerMapper()
        {
            CreateMap<CreateStickerRequest, Sticker>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateStickerRequest, Sticker>().ReverseMap().HandleNullProperty();
            CreateMap<Sticker, StickerResponse>().HandleNullProperty();
            CreateMap<Sticker, CreateStickerResponse>().HandleNullProperty();
        }
    }
}
