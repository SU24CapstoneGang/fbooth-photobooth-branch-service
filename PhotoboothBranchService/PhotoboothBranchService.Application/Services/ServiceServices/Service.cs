using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.ServiceTypeServices
{
    public class Service : IService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public Service(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<CreateServiceTypeResponse> CreateAsync(CreateServiceTypeRequest createModel, StatusUse status)
        {
            var serviceType = _mapper.Map<Domain.Entities.Service>(createModel);
            serviceType.Status = status;
            await _serviceRepository.AddAsync(serviceType);
            return _mapper.Map<CreateServiceTypeResponse>(serviceType);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var serviceTypes = await _serviceRepository.GetAsync(s => s.ServiceID == id);
                var serviceType = serviceTypes.FirstOrDefault();
                if (serviceType != null)
                {
                    await _serviceRepository.RemoveAsync(serviceType);
                }
            }
            catch
            {
                throw;
            }
        }

        // Read all
        public async Task<IEnumerable<ServiceTypeResponse>> GetAllAsync()
        {
            var serviceTypes = await _serviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceTypeResponse>>(serviceTypes.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<ServiceTypeResponse>> GetAllPagingAsync(ServiceTypeFilter filter, PagingModel paging)
        {
            var serviceTypes = (await _serviceRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listServiceTypeResponse = _mapper.Map<IEnumerable<ServiceTypeResponse>>(serviceTypes);
            return listServiceTypeResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<ServiceTypeResponse> GetByIdAsync(Guid id)
        {
            var serviceTypes = await _serviceRepository.GetAsync(s => s.ServiceID == id);
            var serviceType = serviceTypes.FirstOrDefault();
            return _mapper.Map<ServiceTypeResponse>(serviceType);
        }

        public async Task<IEnumerable<ServiceTypeResponse>> GetByName(string name)
        {
            var serviceTypes = await _serviceRepository.GetAsync(s => s.ServiceName.Contains(name));
            return _mapper.Map<IEnumerable<ServiceTypeResponse>>(serviceTypes.ToList());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateServiceTypeRequest updateModel, StatusUse? status)
        {
            var serviceType = (await _serviceRepository.GetAsync(s => s.ServiceID == id)).FirstOrDefault();
            if (serviceType == null)
            {
                throw new KeyNotFoundException("Service type not found.");
            }

            var updatedServiceType = _mapper.Map(updateModel, serviceType);
            if (status.HasValue)
            {
                updatedServiceType.Status = status.Value;
            }
            await _serviceRepository.UpdateAsync(updatedServiceType);
        }
    }
}
