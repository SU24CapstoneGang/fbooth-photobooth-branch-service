using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Order;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.OrderServices
{
    public interface IOrderService : IService<OrderResponse,CreateOrderRequest,UpdateOrderRequest,OrderFilter,PagingModel>
    {
    }
}
