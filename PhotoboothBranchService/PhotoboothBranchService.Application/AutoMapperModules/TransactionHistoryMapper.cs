using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.TransactionHistory;
using PhotoboothBranchService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class TransactionHistoryMapper : Profile
    {
        public TransactionHistoryMapper() {
            CreateMap<CreateTransactionHistoryRequest,TransactionHistory>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateTransactionHistoryRequest, TransactionHistory>().ReverseMap().HandleNullProperty();
            CreateMap<TransactionHistory,TransactionHistoryResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
