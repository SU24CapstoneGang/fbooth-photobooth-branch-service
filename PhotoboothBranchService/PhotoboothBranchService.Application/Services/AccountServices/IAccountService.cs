using PhotoboothBranchService.Application.DTOs;
using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;
using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService : IService<AccountResponse, CreateAccountRequestModel, AccountRegisterResponse, UpdateAccountRequestModel, AccountFilter, PagingModel>
    {
        Task<AccountResponse> GetByEmail(string Email);
        Task<AccountResponse> GetByPhoneNumber(string phoneNumber);
        Task<AccountRegisterResponse> Register(CreateAccountRequestModel request, AccountRole userRole);
        Task<LoginResponeModel> Login(LoginRequestModel request);
        Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request);
        Task<string> ResetPassword(string email);
    }
}
