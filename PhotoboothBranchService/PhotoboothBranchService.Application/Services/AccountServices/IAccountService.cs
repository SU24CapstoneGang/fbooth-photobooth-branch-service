using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService : IServiceBase<AccountResponse, AccountFilter, PagingModel>
    {
        public Task<AccountResponse> GetByEmail(string Email);
        public Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, AccountRole userRole);
        public Task<LoginResponeModel> Login(LoginRequestModel request);
        public Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request);
        public Task<string> ResetPassword(string email);
        public Task UpdateAsync(Guid id, UpdateAccountRequestModel updateModel);
        public Task DeleteAsync(Guid id);
    }
}
