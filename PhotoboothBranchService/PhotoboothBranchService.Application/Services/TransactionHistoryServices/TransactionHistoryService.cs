using AutoMapper;
using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.TransactionHistory;
using PhotoboothBranchService.Domain.Common.Helper;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Application.Services.TransactionHistoryServices
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly IMapper _mapper;

        public TransactionHistoryService(ITransactionHistoryRepository transactionHistoryRepository, IMapper mapper)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(CreateTransactionHistoryRequest createModel)
        {
            TransactionHistory transactionHistory = _mapper.Map<TransactionHistory>(createModel);
            return await _transactionHistoryRepository.AddAsync(transactionHistory);
        }

        public async Task DeleteAsync(Guid id)
        {
            var transactionHistory = await _transactionHistoryRepository.GetAsync(t => t.TransactionID == id);
            var transactionHistoryToDelete = transactionHistory.FirstOrDefault();
            if (transactionHistoryToDelete != null)
            {
                await _transactionHistoryRepository.RemoveAsync(transactionHistoryToDelete);
            }
        }

        public async Task<IEnumerable<TransactionHistoryResponse>> GetAllAsync()
        {
            var transactionHistories = await _transactionHistoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionHistoryResponse>>(transactionHistories.ToList());
        }

        public async Task<IEnumerable<TransactionHistoryResponse>> GetAllPagingAsync(TransactionHistoryFilter filter, PagingModel paging)
        {
            var transactionHistories = (await _transactionHistoryRepository.GetAllAsync()).ToList().AutoFilter(filter);
            var transactionHistoriesResponse = _mapper.Map<IEnumerable<TransactionHistoryResponse>>(transactionHistories);
            transactionHistoriesResponse.AsQueryable().AutoPaging(paging.PageSize, paging.PageIndex);
            return transactionHistoriesResponse;
        }

        public async Task<TransactionHistoryResponse> GetByIdAsync(Guid id)
        {
            var transactionHistories = await _transactionHistoryRepository.GetAsync(t => t.TransactionID == id);
            return _mapper.Map<TransactionHistoryResponse>(transactionHistories);
        }

        public async Task UpdateAsync(Guid id, UpdateTransactionHistoryRequest updateModel)
        {
            var transactionHistory = (await _transactionHistoryRepository.GetAsync(t => t.TransactionID == id)).FirstOrDefault();
            if (transactionHistory == null)
            {
                throw new KeyNotFoundException("Transaction history not found.");
            }
            var updatedTransactionHistory = _mapper.Map(updateModel, transactionHistory);
            await _transactionHistoryRepository.UpdateAsync(updatedTransactionHistory);
        }
    }
}



