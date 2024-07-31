using AutoMapper;
using PhotoboothBranchService.Application.Common.Helpers;
using PhotoboothBranchService.Application.DTOs.Transaction;
using PhotoboothBranchService.Domain.Entities;

namespace PhotoboothBranchService.Application.AutoMapperModules
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {
            CreateMap<CreateTransactionRequest, Transaction>().HandleNullProperty();
            CreateMap<UpdateTransactiontRequest, Transaction>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Transaction, TransactionResponse>().HandleNullProperty();
        }
    }
}
