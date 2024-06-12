﻿using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<Guid> CreateAsync(CreateServiceRequest createModel)
        {
            var service = _mapper.Map<Service>(createModel);
            return await _serviceRepository.AddAsync(service);
        }

        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var services = await _serviceRepository.GetAsync(s => s.ServiceID == id);
                var service = services.FirstOrDefault();
                if (service != null)
                {
                    await _serviceRepository.RemoveAsync(service);
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
            var services = await _serviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceResponse>>(services.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<ServiceResponse>> GetAllPagingAsync(ServiceFilter filter, PagingModel paging)
        {
            var services = (await _serviceRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listServiceResponse = _mapper.Map<IEnumerable<ServiceResponse>>(services);
            listServiceResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return listServiceResponse;
        }

        // Read by ID
        public async Task<ServiceResponse> GetByIdAsync(Guid id)
        {
            var services = await _serviceRepository.GetAsync(s => s.ServiceID == id);
            var service = services.FirstOrDefault();
            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<IEnumerable<ServiceResponse>> GetByName(string name)
        {
            var services = await _serviceRepository.GetAsync(s => s.ServiceName.Contains(name));
            return _mapper.Map<IEnumerable<ServiceResponse>>(services.ToList());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateServiceRequest updateModel)
        {
            var service = (await _serviceRepository.GetAsync(s => s.ServiceID == id)).FirstOrDefault();
            if (service == null)
            {
                throw new KeyNotFoundException("Service not found.");
            }

            var updatedService = _mapper.Map(updateModel, service);
            await _serviceRepository.UpdateAsync(updatedService);
        }
    }
}
