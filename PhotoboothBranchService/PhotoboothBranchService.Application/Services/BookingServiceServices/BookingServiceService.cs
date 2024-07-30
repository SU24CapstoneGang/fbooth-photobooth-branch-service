﻿using AutoMapper;
using Microsoft.VisualBasic;
using PhotoboothBranchService.Application.Common.Exceptions;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.ServiceItem;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;
using PhotoboothBranchService.Domain.IRepository;

namespace PhotoboothBranchService.Application.Services.BookingServiceServices
{
    public class BookingServiceService : IBookingServiceService
    {
        private readonly IBookingServiceRepository _serviceItemRepository;
        private readonly IMapper _mapper;
        private readonly IBookingRepository _sessionOrderRepository;
        private readonly IServiceRepository _serviceTypeRepository;
        private readonly IBoothRepository _boothRepository;
        public BookingServiceService(IBookingServiceRepository serviceItemRepository, IMapper mapper
            , IBookingRepository sessionOrderRepository
            , IServiceRepository serviceTypeRepository, IBoothRepository boothRepository)
        {
            _serviceItemRepository = serviceItemRepository;
            _mapper = mapper;
            _sessionOrderRepository = sessionOrderRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _boothRepository = boothRepository;
        }

        public async Task<CreateBookingServiceResponse> CreateAsync(CreateBookingServiceRequest createModel)
        {
            var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.BookingID == createModel.SessionOrderID)).FirstOrDefault();

            BookingService createServiceItemResponse = null;
            if (sessionOrder != null)
            {
                ValidateOrderToAddServiceItem(sessionOrder);
                //var service = (await ValidateServiceList(new Dictionary<Guid, short>
                //{
                //    {createModel.ServiceID , createModel.Quantity}
                //})).Single();

                BookingService? serviceItem = (await _serviceItemRepository.GetAsync(s => s.ServiceID == createModel.ServiceID
                                            && s.BookingID == sessionOrder.BookingID))
                                            .FirstOrDefault();

                if (serviceItem == null) // create new item 
                {
                    serviceItem = _mapper.Map<BookingService>(createModel);
                    //serviceItem.Price = service.PackagePrice;
                    serviceItem.SubTotal = createModel.Quantity * serviceItem.Price;
                    createServiceItemResponse = await _serviceItemRepository.AddAsync(serviceItem);
                }
                else //update existed item
                {
                    serviceItem.Quantity += createModel.Quantity;
                    serviceItem.SubTotal += createModel.Quantity * serviceItem.Price;
                    await _serviceItemRepository.UpdateAsync(serviceItem);
                    createServiceItemResponse = serviceItem;
                }
            }
            else
            {
                if (sessionOrder == null) throw new NotFoundException("Session Order not found");
            }
            if (createServiceItemResponse != null)
            {
                await _sessionOrderRepository.updateTotalPrice(sessionOrder.BookingID);
            }
            else
            {
                throw new Exception("An unkown error in Service Layer");
            }
            return _mapper.Map<CreateBookingServiceResponse>(createServiceItemResponse);
        }

        public async Task<AddListBookingServiceResponse> AddListServiceItem(AddListBookingServiceRequest request)
        {
            //find now session order of request booth
            var sessionOrder = (await _sessionOrderRepository.GetAsync(i => i.BoothID == request.BoothID /*&& i.Status == BookingStatus.Processsing*/ && i.EndTime > DateTime.Now && DateTime.Now > i.StartTime)).FirstOrDefault();
            if (sessionOrder == null)
            {
                throw new NotFoundException("Not found Session Order running in this booth");
            }
            ValidateOrderToAddServiceItem(sessionOrder);
            //validate list serviceID
            //List<ServicePackage> serviceList = new List<ServicePackage>();
            //if (request.ServiceList.Count > 0)
            //{
            //    serviceList = await ValidateServiceList(request.ServiceList);
            //}
            //else
            //{
            //    throw new BadRequestException("No Service to add");
            //}

            AddListBookingServiceResponse response = new AddListBookingServiceResponse
            {
                BoothID = request.BoothID,
                SessionOrderID = sessionOrder.BookingID,
            };
            foreach (var req in request.ServiceList)
            {
                var serviceItem = (await _serviceItemRepository.GetAsync(i => i.BookingID == sessionOrder.BookingID && i.ServiceID == req.Key)).FirstOrDefault();
                if (serviceItem != null)
                {
                    serviceItem.Quantity += req.Value;
                    serviceItem.SubTotal = serviceItem.Quantity * serviceItem.Price;
                    await _serviceItemRepository.UpdateAsync(serviceItem);
                }
                else
                {
                    serviceItem = new BookingService
                    {
                        Quantity = req.Value,
                        BookingID = sessionOrder.BookingID,
                        ServiceID = req.Key,
                        //Price = serviceList.Find(i => i.ServicePackageID == req.Key).PackagePrice,
                    };
                    serviceItem.SubTotal = serviceItem.Quantity * serviceItem.Price;
                    await _serviceItemRepository.AddAsync(serviceItem);
                }
                response.Items.Add(_mapper.Map<BookingServiceResponse>(serviceItem));
            }
            await _sessionOrderRepository.updateTotalPrice(sessionOrder.BookingID);
            return response;
        }

        //validation
        private void ValidateOrderToAddServiceItem(Booking sessionOrder)
        {
            //if (sessionOrder.EndTime.HasValue && sessionOrder.EndTime.Value < DateTime.Now)
            //{
            //    throw new BadRequestException("Session has end. Please do Payment with our staff and create another Session Order");
            //}
            //else if (sessionOrder.Status == SessionOrderStatus.Done)
            //{
            //    throw new BadRequestException("This Session has been ended, please contect our staff to have new booking");
            //} else if (sessionOrder.Status == SessionOrderStatus.Canceled)
            //{
            //    throw new BadRequestException("This Session has been canceled, please contect our staff to have new booking");
            //}
        }

        //private async Task<List<ServicePackage>> ValidateServiceList(Dictionary<Guid, short> serviceItems)
        //{
        //    List<ServicePackage> serviceList = new List<ServicePackage>();
        //    var serviceIds = serviceItems.Keys.ToList();
        //    serviceList = (await _serviceRepository.GetAsync(i => serviceIds.Contains(i.ServicePackageID), i => i.Service)).ToList();
        //    if (serviceItems.Count() != serviceList.Count)
        //    {
        //        throw new NotFoundException("Some service in request are not found");
        //    }
        //    foreach (var service in serviceList)
        //    {
        //        if (service.Status == StatusUse.Unusable)
        //        {
        //            throw new BadRequestException("Service is not Available to use now");
        //        }
        //        if (service.Service.Status == StatusUse.Unusable)
        //        {
        //            throw new BadRequestException("Service is belong to type that not Available to use now");
        //        }
        //    }
        //    return serviceList;
        //}
        // Delete
        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var serviceItems = await _serviceItemRepository.GetAsync(s => s.BookingServiceID == id);
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
        public async Task<IEnumerable<BookingServiceResponse>> GetAllAsync()
        {
            var serviceItems = await _serviceItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingServiceResponse>>(serviceItems.ToList());
        }

        // Read all with paging and filter
        public async Task<IEnumerable<BookingServiceResponse>> GetAllPagingAsync(BookingServiceFilter filter, PagingModel paging)
        {
            var serviceItems = (await _serviceItemRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var listServiceItemResponse = _mapper.Map<IEnumerable<BookingServiceResponse>>(serviceItems);
            return listServiceItemResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
        }

        // Read by ID
        public async Task<BookingServiceResponse> GetByIdAsync(Guid id)
        {
            var serviceItems = await _serviceItemRepository.GetAsync(s => s.BookingServiceID == id);
            var serviceItem = serviceItems.FirstOrDefault();
            return _mapper.Map<BookingServiceResponse>(serviceItem);
        }

        // Update
        public async Task UpdateAsync(Guid id, UpdateBookingServiceRequest updateModel)
        {
            var serviceItem = (await _serviceItemRepository.GetAsync(s => s.BookingServiceID == id)).FirstOrDefault();
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
            if (updateModel.SessionOrderID.HasValue)
            {
                var order = (await _sessionOrderRepository.GetAsync(i => i.BookingID == updateModel.SessionOrderID)).FirstOrDefault();
                if (order == null)
                {
                    throw new NotFoundException("Not found Service From request");
                }
                //if (order.Status == BookingStatus.Canceled || order.Status == BookingStatus.Done)
                //{
                //    throw new BadRequestException("Session has ended or canceled, can not update");
                //}
            }
            await _serviceItemRepository.UpdateAsync(updatedServiceItem);
        }
    }
}
