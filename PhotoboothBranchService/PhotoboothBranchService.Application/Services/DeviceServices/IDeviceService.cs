using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Device;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.DeviceServices
{
    public interface IDeviceService : IServiceBase<DeviceResponse, DeviceFilter, PagingModel>
    {
        Task<CreateDeviceResponse> CreateAsync(CreateDeviceRequest createModel);
        Task UpdateAsync(Guid id, UpdateDeviceRequest updateModel);
        Task DeleteAsync(Guid id);
    }
}
