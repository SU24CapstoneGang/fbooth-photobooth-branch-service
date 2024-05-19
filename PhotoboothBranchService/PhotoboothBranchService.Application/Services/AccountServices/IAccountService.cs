using PhotoboothBranchService.Application.DTOs.RequestModels.Account;
using PhotoboothBranchService.Application.DTOs.RequestModels.Authentication;
using PhotoboothBranchService.Application.DTOs.ResponseModels.Authentication;

namespace PhotoboothBranchService.Application.Services.AccountServices
{
    public interface IAccountService
    {
        Task<AuthenticationResult> Register(CreateAccountRequestModel createAccountRequestModel, string roleName);
        Task<AuthenticationResult> Login(LoginRequestModel loginRequestModel);
    }
}
