using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.TransactionHistory;
using PhotoboothBranchService.Domain.Common.Interfaces;

namespace PhotoboothBranchService.Application.Services.TransactionHistoryServices
{
    public interface ITransactionHistoryService : IService<TransactionHistoryResponse, CreateTransactionHistoryRequest, UpdateTransactionHistoryRequest, TransactionHistoryFilter, PagingModel>
    {
    }
}
