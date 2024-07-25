﻿using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.SessionOrder;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class SessionOrderMapper : Profile
    {
        public SessionOrderMapper()
        {
            CreateMap<CreateSessionOrderRequest, Booking>()
                .HandleNullProperty();
            CreateMap<UpdateSessionOrderRequest, Booking>().ReverseMap().HandleNullProperty();
            CreateMap<Booking, SessionOrderResponse>().HandleNullProperty();
            CreateMap<Booking, CreateSessionOrderResponse>().HandleNullProperty();
            CreateMap<CustomerBookingSessionOrderRequest, CreateSessionOrderRequest>().HandleNullProperty();
        }
    }
}
