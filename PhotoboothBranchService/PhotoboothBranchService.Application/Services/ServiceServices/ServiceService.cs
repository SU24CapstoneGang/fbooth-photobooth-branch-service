using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.ServiceServices
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<CreateServiceResponse> CreateAsync(CreateServiceRequest createModel, StatusUse status)
        {
            var service = _mapper.Map<Service>(createModel);
            service.Status = status;
            service.ServiceType = ServiceType.Other;
            await _serviceRepository.AddAsync(service);
            return _mapper.Map<CreateServiceResponse>(service);
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
        public async Task<IEnumerable<ServiceResponse>> GetAllAsync()
        {
            var serviceTypes = await _serviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceResponse>>(serviceTypes.ToList().OrderByDescending(i => i.ServiceType));
        }

        // Read all with paging and filter
        public async Task<IEnumerable<ServiceResponse>> GetAllPagingAsync(ServiceFilter filter, PagingModel paging)
        {
            var serviceTypes = (await _serviceRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listServiceTypeResponse = _mapper.Map<IEnumerable<ServiceResponse>>(serviceTypes);
            return listServiceTypeResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex).OrderByDescending(i => i.ServiceType);
        }

        // Read by ID
        public async Task<ServiceResponse> GetByIdAsync(Guid id)
        {
            var serviceTypes = await _serviceRepository.GetAsync(s => s.ServiceID == id);
            var serviceType = serviceTypes.FirstOrDefault();
            return _mapper.Map<ServiceResponse>(serviceType);
        }

        public async Task<IEnumerable<ServiceResponse>> GetByName(string name)
        {
            var serviceTypes = await _serviceRepository.GetAsync(s => s.ServiceName.Contains(name));
            return _mapper.Map<IEnumerable<ServiceResponse>>(serviceTypes.ToList());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateServiceRequest updateModel, StatusUse? status)
        {
            var service = (await _serviceRepository.GetAsync(s => s.ServiceID == id)).FirstOrDefault();
            if (service == null)
            {
                throw new KeyNotFoundException("Service type not found.");
            }
         
            if (service.ServiceType == ServiceType.Printing || service.ServiceType == ServiceType.EmailSending) {
                if (status.HasValue && status == StatusUse.Unusable)
                {
                    throw new BadRequestException("Cannot set this service is Unuseable");
                }
                else
                {
                    await Update(id, updateModel, status, service);
                }
            } else
            {
                await Update(id, updateModel, status, service);
            }
        }

        private async Task Update(Guid id, UpdateServiceRequest updateModel, StatusUse? status, Service service)
        {
            var updatedServiceType = _mapper.Map(updateModel, service);
            if (status.HasValue)
            {
                updatedServiceType.Status = status.Value;
            }
            await _serviceRepository.UpdateAsync(updatedServiceType);
        }
    }
}
