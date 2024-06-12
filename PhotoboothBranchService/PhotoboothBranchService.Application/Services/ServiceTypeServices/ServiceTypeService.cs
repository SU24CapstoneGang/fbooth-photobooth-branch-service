﻿using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceType;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.ServiceTypeServices
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<Guid> CreateAsync(CreateServiceTypeRequest createModel)
        {
            var serviceType = _mapper.Map<ServiceType>(createModel);
            return await _serviceTypeRepository.AddAsync(serviceType);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var serviceTypes = await _serviceTypeRepository.GetAsync(s => s.ServiceTypeID == id);
                var serviceType = serviceTypes.FirstOrDefault();
                if (serviceType != null)
                {
                    await _serviceTypeRepository.RemoveAsync(serviceType);
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
            var serviceTypes = await _serviceTypeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceTypeResponse>>(serviceTypes.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<ServiceTypeResponse>> GetAllPagingAsync(ServiceTypeFilter filter, PagingModel paging)
        {
            var serviceTypes = (await _serviceTypeRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listServiceTypeResponse = _mapper.Map<IEnumerable<ServiceTypeResponse>>(serviceTypes);
            listServiceTypeResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listServiceTypeResponse;
        }

        // Read by ID
        public async Task<ServiceTypeResponse> GetByIdAsync(Guid id)
        {
            var serviceTypes = await _serviceTypeRepository.GetAsync(s => s.ServiceTypeID == id);
            var serviceType = serviceTypes.FirstOrDefault();
            return _mapper.Map<ServiceTypeResponse>(serviceType);
        }

        public async Task<IEnumerable<ServiceTypeResponse>> GetByName(string name)
        {
            var serviceTypes = await _serviceTypeRepository.GetAsync(s => s.ServiceTypeName.Contains(name));
            return _mapper.Map<IEnumerable<ServiceTypeResponse>>(serviceTypes.ToList());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateServiceTypeRequest updateModel)
        {
            var serviceType = (await _serviceTypeRepository.GetAsync(s => s.ServiceTypeID == id)).FirstOrDefault();
            if (serviceType == null)
            {
                throw new KeyNotFoundException("Service type not found.");
            }

            var updatedServiceType = _mapper.Map(updateModel, serviceType);
            await _serviceTypeRepository.UpdateAsync(updatedServiceType);
        }
    }
}
