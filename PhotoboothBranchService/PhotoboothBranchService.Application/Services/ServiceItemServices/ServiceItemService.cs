using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;

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

        public async Task<CreateServiceItemResponse> CreateAsync(CreateServiceItemRequest createModel)
        {
            // Fetch session order, service, and service type asynchronously
            var sessionOrderTask = _sessionOrderRepository.GetAsync(i => i.SessionOrderID == createModel.SessionOrderID);
            var serviceTask = _serviceRepository.GetAsync(i => i.ServiceID == createModel.ServiceID);

            await Task.WhenAll(sessionOrderTask, serviceTask);

            var sessionOrder = sessionOrderTask.Result.FirstOrDefault();
            var service = serviceTask.Result.FirstOrDefault();

            var serviceType = service != null ? (await _serviceTypeRepository.GetAsync(i => i.ServiceTypeID == service.ServiceTypeID)).FirstOrDefault() : null;

            ServiceItem createServiceItemResponse = null;
            if (sessionOrder != null && service != null && serviceType != null)
            {
                if (!serviceType.ServiceTypeName.Equals("Hire booth") && !sessionOrder.EndTime.HasValue)
                {
                    throw new Exception("Please choose the hire booth service first to use the other service");
                }
                else if (sessionOrder.EndTime.HasValue && sessionOrder.EndTime.Value < DateTime.Now)
                {
                    throw new Exception("Session has end. Please choose the hire booth service first to use the other service");
                }
                if (serviceType.ServiceTypeName.Equals("Take photo")) //case add photosession and item service
                {
                    if (!createModel.LayoutID.HasValue)
                    {
                        throw new NotFoundException("No layout in input");
                    }

                    var layout = createModel.LayoutID != null ? (await _layoutRepository.GetAsync(i => i.LayoutID == createModel.LayoutID)).FirstOrDefault() : null;
                    if (layout != null)
                    {
                        //validate time left
                        if (sessionOrder.EndTime.HasValue && sessionOrder.EndTime.Value < DateTime.Now.AddMinutes(service.Measure))
                        {
                            throw new Exception("Your remaining time hire this booth can not enough for use this service ");
                        }
                        var checkPhotoSession = await _photoSessionRepository.GetAsync(i => i.SessionOrderID == sessionOrder.SessionOrderID && i.EndTime < DateTime.Now) == null;
                        if (!checkPhotoSession)
                        {
                            throw new BadRequestException("You are in a another Photosession, can not add more");
                        }
                        // Create photo session
                        var photoSession = new PhotoSession
                        {
                            LayoutID = layout.LayoutID,
                            SessionOrderID = sessionOrder.SessionOrderID,
                            SessionIndex = (await _serviceItemRepository.GetAsync(i => i.SessionOrderID == sessionOrder.SessionOrderID && i.PhotoSessionID != null)).Count() + 1, //count from service item have photo session of this order
                            TotalPhotoTaken = layout.PhotoSlot,
                            StartTime = DateTime.Now.AddMinutes(1), //+1 min in case service delay
                            EndTime = DateTime.Now.AddMinutes(service.Measure),
                        };
                        await _photoSessionRepository.AddAsync(photoSession);

                        //create service item
                        var serviceItem = new ServiceItem
                        {
                            PhotoSessionID = photoSession.PhotoSessionID,
                            Quantity = 1,
                            UnitPrice = service.Price,
                            SubTotal = service.Price,
                            SessionOrderID = sessionOrder.SessionOrderID,
                            ServiceID = service.ServiceID
                        };

                        createServiceItemResponse = await _serviceItemRepository.AddAsync(serviceItem);
                    }
                    else
                    {
                        throw new NotFoundException("Layout not found");
                    }
                }
                else if (serviceType.ServiceTypeName.Equals("Hire booth")) //case hire booth or extend time
                {
                    if (sessionOrder.EndTime.HasValue) //extend time
                    {
                        var serviceItem = (await _serviceItemRepository.GetAsync(s => s.ServiceID == createModel.ServiceID)).FirstOrDefault();
                        if (serviceItem != null)
                        {
                            serviceItem.Quantity += 1;
                            serviceItem.SubTotal = serviceItem.Quantity * serviceItem.UnitPrice;

                            sessionOrder.EndTime = sessionOrder.EndTime.Value.AddMinutes(service.Measure);
                            await _sessionOrderRepository.UpdateAsync(sessionOrder);

                            await _serviceItemRepository.UpdateAsync(serviceItem);
                            createServiceItemResponse = serviceItem;
                        }
                    }
                    else //add new
                    {
                        //update session order
                        sessionOrder.EndTime = sessionOrder.StartTime.AddMinutes(service.Measure);
                        //create service item
                        var serviceItem = _mapper.Map<ServiceItem>(createModel);
                        serviceItem.UnitPrice = service.Price;
                        serviceItem.SubTotal = service.Price;
                        serviceItem.Quantity = 1;
                        await _sessionOrderRepository.UpdateAsync(sessionOrder);
                        createServiceItemResponse = await _serviceItemRepository.AddAsync(serviceItem);
                    }
                }
                else // for other type of service
                {
                    var serviceItem = (await _serviceItemRepository.GetAsync(s => s.ServiceID == createModel.ServiceID && s.SessionOrderID == sessionOrder.SessionOrderID)).FirstOrDefault();
                    if (createModel.Quantity.HasValue)
                    {
                        if (serviceItem == null)
                        {
                            serviceItem = _mapper.Map<ServiceItem>(createModel);
                            serviceItem.UnitPrice = service.Price;
                            serviceItem.SubTotal = createModel.Quantity.Value * serviceItem.UnitPrice;
                            createServiceItemResponse = await _serviceItemRepository.AddAsync(serviceItem);
                        }
                        else
                        {
                            serviceItem.Quantity += createModel.Quantity.Value;
                            serviceItem.SubTotal += createModel.Quantity.Value * serviceItem.UnitPrice;
                            await _serviceItemRepository.UpdateAsync(serviceItem);
                            createServiceItemResponse = serviceItem;
                        }
                    }
                    else
                    {
                        throw new Exception("No quantity input");
                    }

                }
            }
            else
            {
                if (sessionOrder == null) throw new NotFoundException("Session Order not found");
                if (service == null) throw new NotFoundException("Service not found");
                if (serviceType == null) throw new NotFoundException("Service type not found");
            }
            if (createServiceItemResponse != null)
            {
                await _sessionOrderRepository.updateTotalPrice(sessionOrder.SessionOrderID);
            }
            else
            {
                throw new Exception("An unkown error in Service Layer");
            }
            return _mapper.Map<CreateServiceItemResponse>(createServiceItemResponse);
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
