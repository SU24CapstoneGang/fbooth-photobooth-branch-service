using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Camera;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.CameraServices;

public interface ICameraService : IService<Cameraresponse, CreateCameraRequest, UpdateCameraRequest, CameraFilter, PagingModel>
{
    Task<IEnumerable<Cameraresponse>> GetByName(string name);
}
