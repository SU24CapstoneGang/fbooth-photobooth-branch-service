using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Device;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.DeviceServices
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<CreateDeviceResponse> CreateAsync(CreateDeviceRequest createModel)
        {
            var device = _mapper.Map<Device>(createModel);
            await _deviceRepository.AddAsync(device);
            return _mapper.Map<CreateDeviceResponse>(createModel);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var devices = await _deviceRepository.GetAsync(d => d.DeviceID == id);
                var device = devices.FirstOrDefault();
                if (device != null)
                {
                    await _deviceRepository.RemoveAsync(device);
                }
            }
            catch
            {
                throw;
            }
        }

        // Read all
        public async Task<IEnumerable<DeviceResponse>> GetAllAsync()
        {
            var devices = await _deviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DeviceResponse>>(devices.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<DeviceResponse>> GetAllPagingAsync(DeviceFilter filter, PagingModel paging)
        {
            var devices = (await _deviceRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listDeviceResponse = _mapper.Map<IEnumerable<DeviceResponse>>(devices);
            return listDeviceResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<DeviceResponse> GetByIdAsync(Guid id)
        {
            var devices = await _deviceRepository.GetAsync(d => d.DeviceID == id);
            var device = devices.FirstOrDefault();
            return _mapper.Map<DeviceResponse>(device);
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateDeviceRequest updateModel)
        {
            var device = (await _deviceRepository.GetAsync(d => d.DeviceID == id)).FirstOrDefault();
            if (device == null)
            {
                throw new KeyNotFoundException("Device not found.");
            }

            var updatedDevice = _mapper.Map(updateModel, device);
            await _deviceRepository.UpdateAsync(updatedDevice);
        }
    }
}
