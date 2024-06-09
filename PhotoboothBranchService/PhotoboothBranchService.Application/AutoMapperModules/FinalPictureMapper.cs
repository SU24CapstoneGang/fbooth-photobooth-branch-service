using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.FinalPicture;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class FinalPictureMapper : Profile
    {
        public FinalPictureMapper()
        {
            CreateMap<CreateFinalPictureRequest, Photo>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateFinalPictureRequest, Photo>().ReverseMap().HandleNullProperty();
            CreateMap<Photo, FinalPictureResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
