using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
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
        private readonly IBoothRepository _boothRepository;
        private readonly IMapper _mapper;

        public DeviceService(IDeviceRepository deviceRepository, IMapper mapper, IBoothRepository boothRepository)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _boothRepository = boothRepository;
        }

        // Create
        public async Task<CreateDeviceResponse> CreateAsync(CreateDeviceRequest createModel)
        {
            var device = _mapper.Map<Device>(createModel);
            var booth = (await _boothRepository.GetAsync(i => i.BoothID == createModel.BoothID)).SingleOrDefault();
            if (booth == null)
            {
                throw new NotFoundException("Booth not found to create device");
            }
            await _deviceRepository.AddAsync(device);
            return _mapper.Map<CreateDeviceResponse>(device);
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
            return _mapper.Map<IEnumerable<DeviceResponse>>(devices.ToList().OrderByDescending(i => i.CreatedDate));
        }

        // Read all with paging and filter
        public async Task<IEnumerable<DeviceResponse>> GetAllPagingAsync(DeviceFilter filter, PagingModel paging)
        {
            var devices = (await _deviceRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listDeviceResponse = _mapper.Map<IEnumerable<DeviceResponse>>(devices);
            return listDeviceResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).OrderByDescending(i => i.CreatedDate);
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
