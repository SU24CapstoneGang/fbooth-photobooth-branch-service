using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PhotoBox;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoBoxMapper : Profile
    {
        public PhotoBoxMapper()
        {
            CreateMap<CreatePhotoBoxRequest, PhotoBox>().HandleNullProperty();
            CreateMap<UpdatePhotoBoxRequest, PhotoBox>()
    .ForMember(dest => dest.BoxHeight, opt => opt.MapFrom((src, dest) => src.boxHeight.HasValue ? src.boxHeight.Value : dest.BoxHeight))
    .ForMember(dest => dest.BoxWidth, opt => opt.MapFrom((src, dest) => src.boxWidth.HasValue ? src.boxWidth.Value : dest.BoxWidth))
    .ForMember(dest => dest.CoordinatesX, opt => opt.MapFrom((src, dest) => src.CoordinatesX.HasValue ? src.CoordinatesX.Value : dest.CoordinatesX))
    .ForMember(dest => dest.CoordinatesY, opt => opt.MapFrom((src, dest) => src.CoordinatesY.HasValue ? src.CoordinatesY.Value : dest.CoordinatesY))
    .ForMember(dest => dest.IsLandscape, opt => opt.MapFrom((src, dest) => src.IsLandscape.HasValue ? src.IsLandscape.Value : dest.IsLandscape))
    .ForMember(dest => dest.BoxIndex, opt => opt.MapFrom((src, dest) => src.BoxIndex.HasValue ? src.BoxIndex.Value : dest.BoxIndex))
    .ForMember(dest => dest.LayoutID, opt => opt.MapFrom((src, dest) => src.LayoutID.HasValue ? src.LayoutID.Value : dest.LayoutID));
            CreateMap<PhotoBox, PhotoBoxResponse>().HandleNullProperty();
            CreateMap<PhotoBox, CreatePhotoBoxResponse>().HandleNullProperty();
        }
    }
}
