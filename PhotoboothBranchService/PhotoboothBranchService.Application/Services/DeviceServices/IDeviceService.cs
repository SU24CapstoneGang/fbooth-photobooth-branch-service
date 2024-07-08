using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Device;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.DeviceServices
{
    public interface IDeviceService : IServiceBase<DeviceResponse, DeviceFilter, PagingModel>
    {
        public Task<CreateDeviceResponse> CreateAsync(CreateDeviceRequest createModel);
        public Task UpdateAsync(Guid id, UpdateDeviceRequest updateModel);
        public Task DeleteAsync(Guid id);
    }
}
