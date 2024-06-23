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
            CreateMap<UpdatePhotoBoxRequest, PhotoBox>().HandleNullProperty();
            CreateMap<PhotoBox, PhotoBoxResponse>().HandleNullProperty();
            CreateMap<PhotoBox, CreatePhotoBoxResponse>().HandleNullProperty();
        }
    }
}
