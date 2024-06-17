using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
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
        private readonly IPhotoSessionRepository _photoSessionRepository;
        private readonly ILayoutRepository _layoutRepository;
        private readonly ISessionOrderRepository _sessionOrderRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        public ServiceItemService(IServiceItemRepository serviceItemRepository, IMapper mapper, IPhotoSessionRepository photoSessionRepository
            , ILayoutRepository layoutRepository, ISessionOrderRepository sessionOrderRepository
            , IServiceRepository serviceRepository, IServiceTypeRepository serviceTypeRepository)
        {
            _serviceItemRepository = serviceItemRepository;
            _mapper = mapper;
            _photoSessionRepository = photoSessionRepository;
            _layoutRepository = layoutRepository;
            _sessionOrderRepository = sessionOrderRepository;
            _serviceRepository = serviceRepository;
            _serviceTypeRepository = serviceTypeRepository;
        }

        public async Task<Guid> CreateAsync(CreateServiceItemRequest createModel)
        {
            // Fetch session order, service, and service type asynchronously
            var sessionOrderTask = _sessionOrderRepository.GetAsync(i => i.SessionOrderID == createModel.SessionOrderID);
            var serviceTask = _serviceRepository.GetAsync(i => i.ServiceID == createModel.ServiceID);

            await Task.WhenAll(sessionOrderTask, serviceTask);

            var sessionOrder = sessionOrderTask.Result.FirstOrDefault();
            var service = serviceTask.Result.FirstOrDefault();

            var serviceType = service != null ? (await _serviceTypeRepository.GetAsync(i => i.ServiceTypeID == service.ServiceTypeID)).FirstOrDefault() : null;
            var layout = createModel.LayoutID != null ? (await _layoutRepository.GetAsync(i => i.LayoutID == createModel.LayoutID)).FirstOrDefault() : null;

            if (sessionOrder != null && service != null && serviceType != null)
            {
                if (createModel.LayoutID != null && serviceType.ServiceTypeName.Equals("Take photo"))
                {
                    if (layout != null)
                    {
                        // Create photo session
                        var photoSession = new PhotoSession
                        {
                            LayoutID = layout.LayoutID,
                            SessionOrderID = sessionOrder.SessionOrderID,
                            SessionIndex = sessionOrder.ServiceItems.Count + 1,
                            TotalPhotoTaken = layout.PhotoSlot,
                        };
                        photoSession.PhotoSessionID = await _photoSessionRepository.AddAsync(photoSession);

                        var serviceItem = new ServiceItem
                        {
                            PhotoSessionID = photoSession.PhotoSessionID,
                            Quantity = createModel.Quantity,
                            UnitPrice = service.Price,
                            SubTotal = createModel.Quantity * service.Price,
                            SessionOrderID = sessionOrder.SessionOrderID,
                            ServiceID = service.ServiceID
                        };

                        return await _serviceItemRepository.AddAsync(serviceItem);
                    }
                    else
                    {
                        throw new NotFoundException("Layout not found");
                    }
                }
                else
                {
                    var serviceItem = (await _serviceItemRepository.GetAsync(s => s.ServiceID == createModel.ServiceID)).FirstOrDefault();
                    if (serviceItem == null)
                    {
                        serviceItem = _mapper.Map<ServiceItem>(createModel);
                        serviceItem.UnitPrice = service.Price;
                        serviceItem.SubTotal = createModel.Quantity * serviceItem.UnitPrice;
                        sessionOrder.TotalPrice += serviceItem.SubTotal;
                        await _sessionOrderRepository.UpdateAsync(sessionOrder);
                        return await _serviceItemRepository.AddAsync(serviceItem);
                    }
                    else
                    {
                        serviceItem.Quantity += createModel.Quantity;
                        serviceItem.SubTotal += createModel.Quantity * serviceItem.UnitPrice;
                        sessionOrder.TotalPrice += serviceItem.SubTotal;
                        await _sessionOrderRepository.UpdateAsync(sessionOrder);
                        await _serviceItemRepository.UpdateAsync(serviceItem);
                        return serviceItem.ServiceItemID;
                    }
                }
            }
            else
            {
                if (sessionOrder == null) throw new NotFoundException("Session Order not found");
                if (service == null) throw new NotFoundException("Service not found");
                if (serviceType == null) throw new NotFoundException("Service type not found");
            }

            return Guid.Empty;
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
