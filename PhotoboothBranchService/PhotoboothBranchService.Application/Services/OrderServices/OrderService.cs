using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Order;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateOrderRequest createModel)
        {
            Order order = _mapper.Map<Order>(createModel);
            return await _orderRepository.AddAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(o => o.OrderID == id);
            var orderToDelete = order.FirstOrDefault();
            if (orderToDelete != null)
            {
                await _orderRepository.RemoveAsync(orderToDelete);
            }
        }

        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponse>>(orders.ToList());
        }

        public async Task<IEnumerable<OrderResponse>> GetAllPagingAsync(OrderFilter filter, PagingModel paging)
        {
            var orders = (await _orderRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var ordersResponse = _mapper.Map<IEnumerable<OrderResponse>>(orders);
            ordersResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return ordersResponse;
        }

        public async Task<OrderResponse> GetByIdAsync(Guid id)
        {
            var orders = await _orderRepository.GetAsync(o => o.OrderID == id);
            return _mapper.Map<OrderResponse>(orders);
        }

        public async Task UpdateAsync(Guid id, UpdateOrderRequest updateModel)
        {
            var order = (await _orderRepository.GetAsync(o => o.OrderID == id)).FirstOrDefault();
            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            var updatedOrder = _mapper.Map(updateModel, order);
            await _orderRepository.UpdateAsync(updatedOrder);
        }
    }
}
