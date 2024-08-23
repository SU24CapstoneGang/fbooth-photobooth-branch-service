using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Photo;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoMapper : Profile
    {
        public PhotoMapper()
        {
            CreateMap<CreatePhotoRequest, Photo>().ReverseMap().HandleNullProperty();
            CreateMap<UpdatePhotoRequest, Photo>()
    .ForMember(dest => dest.PhotoURL, opt => opt.MapFrom((src, dest) => src.PhotoURL ?? dest.PhotoURL))
    .ForMember(dest => dest.Version, opt => opt.MapFrom((src, dest) => src.Version.HasValue ? src.Version.Value : dest.Version))
    .ForMember(dest => dest.CouldID, opt => opt.MapFrom((src, dest) => src.CouldID ?? dest.CouldID))
    .ForMember(dest => dest.PhotoSessionID, opt => opt.MapFrom((src, dest) => src.PhotoSessionID.HasValue ? src.PhotoSessionID.Value : dest.PhotoSessionID))
    .ForMember(dest => dest.BackgroundID, opt => opt.MapFrom((src, dest) => src.BackgroundID.HasValue ? src.BackgroundID.Value : dest.BackgroundID))
    .ForMember(dest => dest.LastModified, opt => opt.Ignore()); // Typically updated during update

            CreateMap<Photo, PhotoResponse>().HandleNullProperty();
            CreateMap<Photo, CreatePhotoResponse>().HandleNullProperty();
        }
    }
}