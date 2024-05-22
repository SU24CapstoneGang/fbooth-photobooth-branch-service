using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Order;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<CreateOrderRequest, Order>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateOrderRequest, Order>().ReverseMap().HandleNullProperty();
            CreateMap<Order, OrderResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
