using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ServiceItemServices
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly IServiceItemRepository _serviceItemRepository;
        private readonly IMapper _mapper;

        public ServiceItemService(IServiceItemRepository serviceItemRepository, IMapper mapper)
        {
            _serviceItemRepository = serviceItemRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<Guid> CreateAsync(CreateServiceItemRequest createModel)
        {
            var serviceItem = _mapper.Map<ServiceItem>(createModel);
            return await _serviceItemRepository.AddAsync(serviceItem);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var serviceItems = await _serviceItemRepository.GetAsync(s => s.ServiceItemID == id);
                var serviceItem = serviceItems.FirstOrDefault();
                if (serviceItem != null)
                {
                    await _serviceItemRepository.RemoveAsync(serviceItem);
                }
            }
            catch
            {
                throw;
            }
        }

        // Read all
        public async Task<IEnumerable<ServiceItemResponse>> GetAllAsync()
        {
            var serviceItems = await _serviceItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceItemResponse>>(serviceItems.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<ServiceItemResponse>> GetAllPagingAsync(ServiceItemFilter filter, PagingModel paging)
        {
            var serviceItems = (await _serviceItemRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listServiceItemResponse = _mapper.Map<IEnumerable<ServiceItemResponse>>(serviceItems);
            listServiceItemResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listServiceItemResponse;
        }

        // Read by ID
        public async Task<ServiceItemResponse> GetByIdAsync(Guid id)
        {
            var serviceItems = await _serviceItemRepository.GetAsync(s => s.ServiceItemID == id);
            var serviceItem = serviceItems.FirstOrDefault();
            return _mapper.Map<ServiceItemResponse>(serviceItem);
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateServiceItemRequest updateModel)
        {
            var serviceItem = (await _serviceItemRepository.GetAsync(s => s.ServiceItemID == id)).FirstOrDefault();
            if (serviceItem == null)
            {
                throw new KeyNotFoundException("Service item not found.");
            }

            var updatedServiceItem = _mapper.Map(updateModel, serviceItem);
            await _serviceItemRepository.UpdateAsync(updatedServiceItem);
        }
    }
}
