﻿using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Payment;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class PaymentMapper : Profile
    {
        public PaymentMapper()
        {
            CreateMap<CreatePaymentRequest, Transaction>().HandleNullProperty();
            CreateMap<UpdatePaymentRequest, Transaction>().HandleNullProperty();
            CreateMap<Transaction, PaymentResponse>().HandleNullProperty();
        }
    }
}
