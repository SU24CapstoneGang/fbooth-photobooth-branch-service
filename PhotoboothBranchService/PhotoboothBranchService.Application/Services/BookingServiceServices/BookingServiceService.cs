using AutoMapper;
using Microsoft.VisualBasic;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.BookingService;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.BookingServiceServices
{
    public class BookingServiceService : IBookingServiceService
    {
        private readonly IBookingServiceRepository _bookingServiceRepository;
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IBoothRepository _boothRepository;
        public BookingServiceService(IBookingServiceRepository serviceItemRepository, IMapper mapper
            , IBookingRepository sessionOrderRepository
            , IServiceRepository serviceTypeRepository, IBoothRepository boothRepository)
        {
            _bookingServiceRepository = serviceItemRepository;
            _mapper = mapper;
            _bookingRepository = sessionOrderRepository;
            _serviceRepository = serviceTypeRepository;
            _boothRepository = boothRepository;
        }

        public async Task<CreateBookingServiceResponse> CreateAsync(CreateBookingServiceRequest createModel)
        {
            var sessionOrder = (await _bookingRepository.GetAsync(i => i.BookingID == createModel.BookingID)).FirstOrDefault();

            BookingService createServiceItemResponse = null;
            if (sessionOrder != null)
            {
                BookingService? serviceItem = (await _bookingServiceRepository.GetAsync(s => s.ServiceID == createModel.ServiceID
                                            && s.BookingID == sessionOrder.BookingID))
                                            .FirstOrDefault();

                if (serviceItem == null) // create new item 
                {
                    serviceItem = _mapper.Map<BookingService>(createModel);
                    //serviceItem.Price = service.PackagePrice;
                    serviceItem.SubTotal = createModel.Quantity * serviceItem.Price;
                    createServiceItemResponse = await _bookingServiceRepository.AddAsync(serviceItem);
                }
                else //update existed item
                {
                    serviceItem.Quantity += createModel.Quantity;
                    serviceItem.SubTotal += createModel.Quantity * serviceItem.Price;
                    await _bookingServiceRepository.UpdateAsync(serviceItem);
                    createServiceItemResponse = serviceItem;
                }
            }
            else
            {
                if (sessionOrder == null) throw new NotFoundException("Session Order not found");
            }
            if (createServiceItemResponse != null)
            {
            }
            else
            {
                throw new Exception("An unkown error in Service Layer");
            }
            return _mapper.Map<CreateBookingServiceResponse>(createServiceItemResponse);
        }

        public async Task AddByList(List<BookingService> resquestList, Guid bookingID)
        {
            var addTasks = resquestList.Select(async bookingService =>
            {
                bookingService.BookingID = bookingID;
                await _bookingServiceRepository.AddAsync(bookingService);
            });
            await Task.WhenAll(addTasks);
        }

        public async Task<decimal> AddExtraService(AddListBookingServiceRequest request)
        {
           
            var sList = await this.ValidateServiceList(request.ServiceList);
            decimal total = 0;
            foreach (var service in sList)
            {
                var bookingService = (await _bookingServiceRepository.GetAsync(i => i.BookingID == request.BookingID && i.ServiceID == service.ServiceID)).FirstOrDefault();
                if (bookingService != null)
                {
                    bookingService.Quantity += request.ServiceList[service.ServiceID];
                    bookingService.SubTotal = bookingService.Quantity * bookingService.Price;
                    await _bookingServiceRepository.UpdateAsync(bookingService);
                    total += request.ServiceList[service.ServiceID] * bookingService.Price;
                }
                else
                {
                    bookingService = new BookingService
                    {
                        Quantity = request.ServiceList[service.ServiceID],
                        BookingID = request.BookingID,
                        ServiceID = service.ServiceID,
                        Price = service.ServicePrice,
                    };
                    bookingService.SubTotal = bookingService.Quantity * bookingService.Price;
                    total += bookingService.SubTotal;
                    await _bookingServiceRepository.AddAsync(bookingService);
                }
            }
            return total;
        }
        public async Task<(decimal, ICollection<BookingService>)> CreateServiceListForNewBooking(Dictionary<Guid, short> serviceList)
        {

            List<BookingService> result = new List<BookingService>();

            var bookingAmount = 0m;
            if (serviceList.Any())
            {
                
                var services = await this.ValidateServiceList(serviceList);

                if (serviceList.Count() != services.Count())
                {
                    throw new BadRequestException("Service(s) from input not found.");
                }
                foreach (var service in services)
                {
                    var subtotal = service.ServicePrice * serviceList[service.ServiceID];
                    BookingService bookingService = new BookingService
                    {
                        ServiceID = service.ServiceID,
                        Quantity = serviceList[service.ServiceID],
                        SubTotal = subtotal,
                        Price = service.ServicePrice,
                    };
                    result.Add(bookingService);
                    bookingAmount += subtotal;
                }
            }

            return (bookingAmount, result);
        }

        private async Task<List<Service>> ValidateServiceList(Dictionary<Guid, short> serviceItems)
        {
            var serviceIds = serviceItems.Keys.ToList();
            var serviceList = (await _serviceRepository.GetAsync(i => serviceIds.Contains(i.ServiceID))).ToList();
            if (serviceItems.Count() != serviceList.Count)
            {
                throw new NotFoundException("Some service in request are not found");
            }

            if (serviceList.Any(i => i.Status == StatusUse.Unusable))
            {
                throw new BadRequestException("Service is not Available to use now");
            }

            return serviceList;
        }
        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var serviceItems = await _bookingServiceRepository.GetAsync(s => s.BookingServiceID == id);
                var serviceItem = serviceItems.FirstOrDefault();
                if (serviceItem != null)
                {
                    await _bookingServiceRepository.RemoveAsync(serviceItem);
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteByBookingIdAsync(Guid BookingID)
        {
            var bookingServices = (await _bookingServiceRepository.GetAsync(i => i.BookingID == BookingID)).ToList();
            var removalTasks = bookingServices.Select(bookingService =>
                _bookingServiceRepository.RemoveAsync(bookingService)
            );
            await Task.WhenAll(removalTasks);
        }
        // Read all
        public async Task<IEnumerable<BookingServiceResponse>> GetAllAsync()
        {
            var serviceItems = await _bookingServiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingServiceResponse>>(serviceItems.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<BookingServiceResponse>> GetAllPagingAsync(BookingServiceFilter filter, PagingModel paging)
        {
            var serviceItems = (await _bookingServiceRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listServiceItemResponse = _mapper.Map<IEnumerable<BookingServiceResponse>>(serviceItems);
            return listServiceItemResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<BookingServiceResponse> GetByIdAsync(Guid id)
        {
            var serviceItems = await _bookingServiceRepository.GetAsync(s => s.BookingServiceID == id);
            var serviceItem = serviceItems.FirstOrDefault();
            return _mapper.Map<BookingServiceResponse>(serviceItem);
        }
        public async Task<IEnumerable<BookingService>> GetByBookingIdAsync(Guid BookingID)
        {
            var serviceItems = await _bookingServiceRepository.GetAsync(s => s.BookingID == BookingID, i => i.Service);
            return serviceItems.ToList();
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateBookingServiceRequest updateModel)
        {
            var serviceItem = (await _bookingServiceRepository.GetAsync(s => s.BookingServiceID == id)).FirstOrDefault();
            if (serviceItem == null)
            {
                throw new NotFoundException("Service item not found.");
            }

            var updatedServiceItem = _mapper.Map(updateModel, serviceItem);
            if (updateModel.ServiceID.HasValue && updateModel.ServiceID != serviceItem.ServiceID)
            {
                //var service = (await _serviceRepository.GetAsync(i => i.ServicePackageID == updateModel.ServiceID)).FirstOrDefault();
                //if (service == null)
                //{
                //    throw new NotFoundException("Not found Service From request");
                //}
            }
            if (updateModel.BookingID.HasValue)
            {
                var order = (await _bookingRepository.GetAsync(i => i.BookingID == updateModel.BookingID)).FirstOrDefault();
                if (order == null)
                {
                    throw new NotFoundException("Not found Service From request");
                }
                //if (order.Status == BookingStatus.Canceled || order.Status == BookingStatus.Done)
                //{
                //    throw new BadRequestException("Session has ended or canceled, can not update");
                //}
            }
            await _bookingServiceRepository.UpdateAsync(updatedServiceItem);
        }
    }
}
