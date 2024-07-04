using AutoMapper;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.ServiceItemServices
{
    public class ServiceItemService : IServiceItemService
    {
        private readonly IServiceItemRepository _serviceItemRepository;
        private readonly IMapper _mapper;
        private readonly ISessionOrderRepository _sessionOrderRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IBoothRepository _boothRepository;
        public ServiceItemService(IServiceItemRepository serviceItemRepository, IMapper mapper
            , ISessionOrderRepository sessionOrderRepository
            , IServiceRepository serviceRepository, IServiceTypeRepository serviceTypeRepository, IBoothRepository boothRepository)
        {
            _serviceItemRepository = serviceItemRepository;
            _mapper = mapper;
            _sessionOrderRepository = sessionOrderRepository;
            _serviceRepository = serviceRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _boothRepository = boothRepository;
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

                if (sessionOrder.EndTime.HasValue && sessionOrder.EndTime.Value < DateTime.Now)
                {
                    throw new Exception("Session has end. Please do Payment with our staff and create another Session Order");
                }
                else if (sessionOrder.Status == Domain.Enum.SessionOrderStatus.Done)
                {
                    throw new Exception("This Session has been ended, please contect our staff to have new booking");
                }

                if (createModel.Quantity.HasValue)
                {
                    ServiceItem? serviceItem = (await _serviceItemRepository.GetAsync(s => s.ServiceID == createModel.ServiceID
                                                && s.SessionOrderID == sessionOrder.SessionOrderID))
                                                .FirstOrDefault();

                    if (serviceItem == null) // create new item 
                    {
                        serviceItem = _mapper.Map<ServiceItem>(createModel);
                        serviceItem.UnitPrice = service.Price;
                        serviceItem.SubTotal = createModel.Quantity.Value * serviceItem.UnitPrice;
                        createServiceItemResponse = await _serviceItemRepository.AddAsync(serviceItem);
                    }
                    else //update existed item
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

        public async Task<AddListServiceItemResponse> AddListServiceItem(AddListServiceItemRequest request)
        {
            //find now session order of request booth
            var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.BoothID == request.BoothID && i.Status == SessionOrderStatus.Processsing &&(i.EndTime > DateTime.Now && DateTime.Now > i.StartTime))).FirstOrDefault();
            if (sessionOrder == null) {
                throw new NotFoundException("Not found Session Order running in this booth");
            }
            //validate list serviceID
            List<Service> serviceList = new List<Service>();
            if (request.ServiceList.Count > 0)
            {
                var serviceIds = request.ServiceList.Keys.ToList();
                var services = await _serviceRepository.GetAsync(i => serviceIds.Contains(i.ServiceID));
                if (request.ServiceList.Count != services.Count())
                {
                    throw new Exception("Some service in request are not found");
                } else
                {
                    serviceList = services.ToList();
                }
            }
            else
            {
                throw new Exception("No Service to add");
            }

            AddListServiceItemResponse response = new AddListServiceItemResponse { 
                BoothID = request.BoothID,
                SessionOrderID = sessionOrder.SessionOrderID,
            };
            foreach (var req in request.ServiceList)
            {
                var serviceItem = (await _serviceItemRepository.GetAsync(i => i.SessionOrderID == sessionOrder.SessionOrderID && i.ServiceID == req.Key)).FirstOrDefault();
                if (serviceItem != null)
                {
                    serviceItem.Quantity += req.Value;
                    serviceItem.SubTotal = serviceItem.Quantity * serviceItem.UnitPrice;
                    await _serviceItemRepository.UpdateAsync(serviceItem);
                } else
                {
                    serviceItem = new ServiceItem
                    {
                        Quantity = req.Value,
                        SessionOrderID = sessionOrder.SessionOrderID,
                        ServiceID = req.Key,
                        UnitPrice = serviceList.Find(i => i.ServiceID == req.Key).Price,
                    };
                    serviceItem.SubTotal = serviceItem.Quantity * serviceItem.UnitPrice;
                    await _serviceItemRepository.AddAsync(serviceItem);
                }
                response.Items.Add(_mapper.Map<ServiceItemResponse>(serviceItem));
            }
            await _sessionOrderRepository.updateTotalPrice(sessionOrder.SessionOrderID);
            return response;
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
            return listServiceItemResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
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
