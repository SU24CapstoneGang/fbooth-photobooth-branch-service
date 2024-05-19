using PhotoboothBranchService.Application.DTOs.Account;
using PhotoboothBranchService.Application.DTOs.Authentication;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService
    {
        Task<AccountRespone> Register(CreateAccountRequestModel request);
        Task<LoginResponeModel> Login(LoginRequestModel request);
        Task<LoginResponeModel> RefreshToken(RefreshTokenRequestModel request);
    }
}
