using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Camera;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules;

public class CameraMapper : Profile
{
    public CameraMapper()
    {
        CreateMap<CreateCameraRequest, Camera>().ReverseMap().HandleNullProperty();
        CreateMap<UpdateCameraRequest, Camera>().ReverseMap().HandleNullProperty();
        CreateMap<Cameraresponse, Camera>().ReverseMap().HandleNullProperty();
    }
}
