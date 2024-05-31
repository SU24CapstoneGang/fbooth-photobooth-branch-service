using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.TransactionHistory;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class TransactionHistoryMapper : Profile
    {
        public TransactionHistoryMapper()
        {
            CreateMap<CreateTransactionHistoryRequest, TransactionHistory>().ReverseMap().HandleNullProperty();
            CreateMap<UpdateTransactionHistoryRequest, TransactionHistory>().ReverseMap().HandleNullProperty();
            CreateMap<TransactionHistory, TransactionHistoryResponse>().ReverseMap().HandleNullProperty();
        }
    }
}
