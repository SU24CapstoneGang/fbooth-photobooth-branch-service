﻿using AutoMapper;
using CloudinaryDotNet;
using OpenCvSharp;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Service;
using PhotoboothBranchService.Application.Services.CloudinaryServices;
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
        private readonly ICloudinaryService _cloudinaryService;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        // Create
        public async Task<CreateServiceResponse> CreateAsync(CreateServiceRequest createModel)
        {
            var service = _mapper.Map<Service>(createModel);
            service.ServiceType = (ServiceType)createModel.ServiceType;

            //upload to cloudinary
            var uploadResult = await _cloudinaryService.AddPhotoAsync(createModel.imgFile, "FBooth-Service");
            if (uploadResult.Error != null)
            {
                throw new Exception(uploadResult.Error.Message);
            }
            service.CouldID = uploadResult.PublicId;
            service.ServiceIamgeURL = uploadResult.SecureUrl.AbsoluteUri;
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
            if (serviceType == null) 
            {
                throw new NotFoundException("Service not found");
            }
            return _mapper.Map<ServiceResponse>(serviceType);
        }

        public async Task<IEnumerable<ServiceResponse>> GetByName(string name)
        {
            var serviceTypes = await _serviceRepository.GetAsync(s => s.ServiceName.Contains(name));
            return _mapper.Map<IEnumerable<ServiceResponse>>(serviceTypes.ToList());
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateServiceRequest updateModel)
        {
            var service = (await _serviceRepository.GetAsync(s => s.ServiceID == id)).FirstOrDefault();
            if (service == null)
            {
                throw new KeyNotFoundException("Service not found.");
            }
            bool check = false;
            if (service.ServiceType == ServiceType.Printing || service.ServiceType == ServiceType.EmailSending) {
                if (updateModel.Status.HasValue && updateModel.Status == StatusUse.Unusable)
                {
                    throw new BadRequestException("Cannot set this service is Unuseable");
                }
                else
                {
                    check = true;
                }
            } else
            {
                check = true;
            }
            if (check)
            {
                if (updateModel.imgFile != null && updateModel.imgFile.Length != 0)
                {
                    var result = await _cloudinaryService.UpdatePhotoAsync(updateModel.imgFile, service.CouldID);
                    service.ServiceIamgeURL = result.SecureUrl.AbsoluteUri;
                }
                await Update(updateModel, service);
            }
        }

        private async Task Update(UpdateServiceRequest updateModel, Service service)
        {
            var updatedServiceType = _mapper.Map(updateModel, service);
            await _serviceRepository.UpdateAsync(updatedServiceType);
        }
    }
}
