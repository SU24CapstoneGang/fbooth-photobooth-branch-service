using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.PhotoSession;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PhotoSessionMapper : Profile
    {
        public PhotoSessionMapper()
        {
            CreateMap<CreatePhotoSessionRequest, PhotoSession>().HandleNullProperty();
            CreateMap<UpdatePhotoSessionRequest, PhotoSession>().HandleNullProperty();
            CreateMap<PhotoSession, PhotoSessionResponse>().HandleNullProperty();
            CreateMap<PhotoSession, CreatePhotoSessionResponse>().HandleNullProperty();
        }
    }
}
