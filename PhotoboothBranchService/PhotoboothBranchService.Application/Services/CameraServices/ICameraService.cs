using PhotoboothBranchService.Application.DTOs.RequestModels;
using PhotoboothBranchService.Application.DTOs.RequestModels.Camera;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Camera;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.CameraServices;

public interface ICameraService : IService<Cameraresponse, CreateCameraRequest, UpdateCameraRequest, CameraFilter, PagingModel>
{
    Task<IEnumerable<Cameraresponse>> GetByName(string name);
}
