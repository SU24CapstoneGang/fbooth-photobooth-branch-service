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
            CreateMap<UpdatePhotoRequest, Photo>().ReverseMap().HandleNullProperty();
            CreateMap<Photo, PhotoResponse>().HandleNullProperty();
            CreateMap<Photo, CreatePhotoResponse>().HandleNullProperty();
        }
    }
}