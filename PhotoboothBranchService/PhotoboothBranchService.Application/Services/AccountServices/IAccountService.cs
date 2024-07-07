using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService : IServiceBase<AccountResponse, AccountFilter, PagingModel>
    {
        Task<AccountResponse> GetByEmail(string Email);
        Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, AccountRole userRole);
        Task<LoginResponeModel> Login(LoginRequestModel request);
        Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request);
        Task<string> ResetPassword(string email);
        public Task UpdateAsync(Guid id, UpdateAccountRequestModel updateModel);
        public Task DeleteAsync(Guid id);
    }
}
