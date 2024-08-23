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
            CreateMap<UpdateStickerRequest, Sticker>()
                .ForMember(dest => dest.StickerCode, opt => opt.MapFrom((src, dest) => src.StickerCode ?? dest.StickerCode))
                .ForMember(dest => dest.StickerURL, opt => opt.MapFrom((src, dest) => src.StickerURL ?? dest.StickerURL))
                .ForMember(dest => dest.CouldID, opt => opt.MapFrom((src, dest) => src.CouldID ?? dest.CouldID))
                .ForMember(dest => dest.stickerHeight, opt => opt.MapFrom((src, dest) => src.stickerHeight.HasValue ? src.stickerHeight.Value : dest.stickerHeight))
                .ForMember(dest => dest.stickerWidth, opt => opt.MapFrom((src, dest) => src.stickerWidth.HasValue ? src.stickerWidth.Value : dest.stickerWidth))
                .ForMember(dest => dest.Status, opt => opt.MapFrom((src, dest) => src.Status.HasValue ? src.Status.Value : dest.Status))
                .ForMember(dest => dest.StickerTypeID, opt => opt.MapFrom((src, dest) => src.StickerTypeID.HasValue ? src.StickerTypeID.Value : dest.StickerTypeID))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Typically set during creation
                .ForMember(dest => dest.LastModified, opt => opt.Ignore()); // Typically updated during update

            CreateMap<Sticker, StickerResponse>().HandleNullProperty();
            CreateMap<Sticker, CreateStickerResponse>().HandleNullProperty();
        }
    }
}
