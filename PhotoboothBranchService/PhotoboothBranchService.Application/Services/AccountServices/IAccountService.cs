using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService : IServiceBase<AccountResponse, AccountFilter, PagingModel>
    {
        Task<Account> GetByEmail(string Email);
        Task<AccountResponse> GetByPhoneNumber(string phoneNumber);
        Task UpdateAsync(UpdateAccountRequestModel updateModel, string? email);
        Task DeleteAsync(Guid id);
        Task AssignBranchForStaff(AssignBranchForStaffRequest request);
        Task<Account> ValidateCustomerAsync(string? phoneNumber, string? email);
    }
}
