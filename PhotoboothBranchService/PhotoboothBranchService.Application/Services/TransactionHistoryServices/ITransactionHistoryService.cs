using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.TransactionHistory;
using PhotoboothBranchService.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.TransactionHistoryServices
{
    public interface ITransactionHistoryService : IService<TransactionHistoryResponse, CreateTransactionHistoryRequest, UpdateTransactionHistoryRequest, TransactionHistoryFilter, PagingModel>
    {
    }
}
